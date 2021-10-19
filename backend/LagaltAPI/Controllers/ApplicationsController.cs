using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Application;
using LagaltAPI.Models.Wrappers;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationService _applicationService;
        private readonly UriService _uriService;

        public ApplicationsController(
            IMapper mapper, ApplicationService applicationService, UriService uriService)
        {
            _mapper = mapper;
            _applicationService = applicationService;
            _uriService = uriService;
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
                var domainApplication = await _applicationService.GetByIdAsync(applicationId);

                if (domainApplication != null)
                    return _mapper.Map<ApplicationReadDTO>(domainApplication);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
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
            var baseUri = _uriService.GetBaseUrl() + $"api/Applications/Project/{projectId}";
            return new Page<ApplicationReadDTO>(applications, filter, baseUri);
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
            domainApplication = await _applicationService.AddAsync(domainApplication);

            return CreatedAtAction("GetApplication",
                new { applicationId = domainApplication.Id },
                _mapper.Map<ApplicationReadDTO>(domainApplication));
        }
    }
}
