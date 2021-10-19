using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Application;
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
        private readonly ApplicationService _service;

        public ApplicationsController(IMapper mapper, ApplicationService service)
        {
            _mapper = mapper;
            _service = service;
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
                var domainApplication = await _service.GetByIdAsync(applicationId);

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

        /// <summary> Fetches applications from the database based on project id. </summary>
        /// <param name="projectId"> The id of the project to retrieve applications from. </param>
        /// <returns> An enumerable containing read-specific DTOs of the applications. </returns>
        // GET: api/Applications/Project/5
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ApplicationReadDTO>>> GetProjectApplications(
            int projectId)
        {
            return _mapper.Map<List<ApplicationReadDTO>>(
                await _service.GetByProjectIdAsync(projectId));
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
            if(_service.HasUserAppliedToProject(dtoApplication.UserId, dtoApplication.ProjectId)) {
                return BadRequest("Existing application for user found in the projcet");
            }

            var domainApplication = _mapper.Map<Application>(dtoApplication);
            domainApplication = await _service.AddAsync(domainApplication);

            return CreatedAtAction("GetApplication",
                new { applicationId = domainApplication.Id },
                _mapper.Map<ApplicationReadDTO>(domainApplication));
        }
    }
}
