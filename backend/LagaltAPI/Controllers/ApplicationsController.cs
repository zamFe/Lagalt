using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Application;
using LagaltAPI.Models.Wrappers;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationService _applicationService;
        private readonly ProjectService _projectService;
        private readonly UriService _uriService;
        private readonly UserService _userService;

        public ApplicationsController(
            IMapper mapper, ApplicationService applicationService, ProjectService projectService,
            UriService uriService, UserService userService)
        {
            _mapper = mapper;
            _applicationService = applicationService;
            _projectService = projectService;
            _uriService = uriService;
            _userService = userService;
        }

        private  ValidationResult ValidateNewApplication(Application application)
        {
            if (!_userService.UserExists(application.UserId))
                return new ValidationResult(false, "Unable to find user");

            if (!_projectService.ProjectExists(application.ProjectId))
                return new ValidationResult(false, "Unable to find project");

            if (_applicationService.UserHasAppliedToProject(
                application.UserId, application.ProjectId))
            {
                return new ValidationResult(false, "User has already applied to project");
            }

            return new ValidationResult(true);
        }

        /// <summary> Checks whether an update-specific DTO is properly formated. </summary>
        /// <param name="endpoint"> The endpoint at which the PUT request was recieved. </param>
        /// <param name="dtoApplication">
        ///     An edit-specific DTO containing an updated version of an application.
        /// </param>
        /// <param name="domainApplication">
        ///     The current version of the application to be updated.
        /// </param>
        /// <returns> A ValidationResult with a result and the reason for the result. </returns>
        private ValidationResult ValidateUpdatedApplication(
            int endpoint, ApplicationEditDTO dtoApplication, Application domainApplication)
        {
            if (endpoint != dtoApplication.Id)
            {
                return new ValidationResult(
                    false, "Mismatch between application id and API endpoint");
            }

            if (domainApplication.Accepted)
                return new ValidationResult(false, "Application has already been accepted");

            return new ValidationResult(true);
        }

        /// <summary> Fetches an application from the database based on application id. </summary>
        /// <param name="applicationId"> The id of the application to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the application if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Applications/5
        [HttpGet("{applicationId}")]
        public async Task<ActionResult<ApplicationReadDTO>> GetApplication(int applicationId)
        {
            try
            {
                var domainApplication = await _applicationService.GetReadonlyByIdAsync(applicationId);

                if (domainApplication != null)
                    return _mapper.Map<ApplicationReadDTO>(domainApplication);
            }
            catch (InvalidOperationException) {}
            return NotFound();
        }

        /// <summary>
        ///     Fetches applications from the database based on project id,
        ///     according to the specified offset and limit.
        /// </summary>
        /// <param name="projectId"> The id of the project to retrieve applications from. </param>
        /// <param name="offset">
        ///     Specifies the index of the first application to be included.
        /// </param>
        /// <param name="limit"> Specifies how many applications to include. </param>
        /// <returns> An enumerable containing read-specific DTOs of the applications. </returns>
        // GET: api/Applications/Project/5?offset=5&limit=5
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<Page<ApplicationReadDTO>>> GetProjectApplications(
            int projectId, [FromQuery] int offset, [FromQuery] int limit)
        {
            var filter = new PageRange(offset, limit);
            var applications = _mapper.Map<List<ApplicationReadDTO>>(
                await _applicationService.GetPageByProjectIdAsync(projectId, filter));
            var totalApplications = await _applicationService.GetTotalProjectApplicationsAsync(
                projectId);
            var baseUri = _uriService.GetBaseUrl() + $"api/Applications/Project/{projectId}";
            return new Page<ApplicationReadDTO>(applications, totalApplications, filter, baseUri);
        }

        /// <summary> Adds a new application entry to the database. </summary>
        /// <param name="dtoApplication">
        ///     A creation-specific DTO representing the new application.
        /// </param>
        /// <returns>
        ///     A read-specific DTO of the application just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Applications
        [HttpPost]
        public async Task<ActionResult<ApplicationReadDTO>> PostApplication(
            ApplicationCreateDTO dtoApplication)
        {
            var domainApplication = _mapper.Map<Application>(dtoApplication);

            var validation = ValidateNewApplication(domainApplication);
            if(!validation.Result)
                return BadRequest(validation.RejectionReason);

            domainApplication = await _applicationService.AddAsync(domainApplication);

            return CreatedAtAction("GetApplication",
                new { applicationId = domainApplication.Id },
                _mapper.Map<ApplicationReadDTO>(domainApplication));
        }

        /// <summary>
        ///     Updates the specified application in the database to match the provided DTO.
        /// </summary>
        /// <param name="applicationId"> The id of the application to update. </param>
        /// <param name="dtoApplication">
        ///     An edit-specific DTO containing the updated version of the application.
        /// </param>
        /// <returns>
        ///     NoContent on successful database update,
        ///     BadRequest if the applicationId and the id of the dto do not match
        ///     or if the application has already been accepted,
        ///     or NotFound if the provided application id does not match
        ///     any applications in the database.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     Thrown when the application is found in the database but not able to be updated.
        /// </exception>
        // PUT: api/Applications/5
        [HttpPut("{applicationId}")]
        public async Task<IActionResult> PutUser(
            int applicationId, ApplicationEditDTO dtoApplication)
        {
            if (!_applicationService.ApplicationExists(applicationId))
                return NotFound();

            var domainApplication = await _applicationService.GetWriteableByIdAsync(applicationId);

            var validation = ValidateUpdatedApplication(
                applicationId, dtoApplication, domainApplication);
            if (!validation.Result)
                return BadRequest(validation.RejectionReason);
            
            var newlyAccepted = dtoApplication.Accepted && !domainApplication.Accepted;
            _mapper.Map<ApplicationEditDTO, Application>(dtoApplication, domainApplication);

            try
            {
                await _applicationService.UpdateAsync(domainApplication, newlyAccepted);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_applicationService.ApplicationExists(applicationId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
