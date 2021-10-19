using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Project;
using LagaltAPI.Models.Wrappers;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ProjectService _projectService;
        private readonly UriService _uriService;

        // Constructor.
        public ProjectsController(
            IMapper mapper, ProjectService projectService, UriService uriService)
        {
            _mapper = mapper;
            _projectService = projectService;
            _uriService = uriService;
        }

        /// <summary> Fetches a project from the database based on project id. </summary>
        /// <param name="projectId"> The id of the project to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the project if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Projects/5
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectCompleteReadDTO>> GetProject(int projectId)
        {
            try
            {
                var domainProject = await _projectService.GetByIdAsync(projectId);

                if (domainProject != null)
                    return _mapper.Map<ProjectCompleteReadDTO>(domainProject);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Fetches projects from the database according to the specified offset and limit.
        /// </summary>
        /// <param name="offset"> Specifies the index of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to include. </param>
        /// <returns>
        ///     A page containing all available read-specific DTOs within the specified range.
        /// </returns>
        // GET api/Projects?offset=5&limit=5
        [HttpGet]
        public async Task<ActionResult<Page<ProjectCompactReadDTO>>> GetProjects(
            [FromQuery] int offset, [FromQuery] int limit)
        {
            var range = new PageRange(offset, limit);
            var projects = _mapper.Map<List<ProjectCompactReadDTO>>(
                await _projectService.GetPageAsync(range));
            var baseUri = _uriService.GetBaseUrl() + "api/Projects";
            return new Page<ProjectCompactReadDTO>(projects, range, baseUri);
        }

        /// <summary> Generates recommended projects for a user. </summary>
        /// <param name="userId"> The id of the user to get recommended projects for. </param>
        /// <param name="offset"> Specifies the index of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to include. </param>
        /// <returns>
        ///     A page containing all available read-specific DTOs for the user.
        /// </returns>
        // GET: api/Projects/Recommended/User/5
        [HttpGet("Recommended/User/{userId}")]
        public async Task<ActionResult<Page<ProjectCompactReadDTO>>> GetRecommendedProjects(
            int userId, [FromQuery] int offset, [FromQuery] int limit)
        {
            var range = new PageRange(offset, limit);
            var projects = _mapper.Map<List<ProjectCompactReadDTO>>(
                await _projectService.GetRecommendedProjectsPageAsync(userId, range));
            var baseUri = _uriService.GetBaseUrl() + $"api/Projects/Recommended/User/{userId}";
            return new Page<ProjectCompactReadDTO>(projects, range, baseUri);
        }

        /// <summary> Fetches a user's projects from the database. </summary>
        /// <param name="userId"> The id of the user to retrieve projects for. </param>
        /// <returns>
        ///     An enumerable containing read-specific DTOs of the projects joined by the user.
        /// </returns>
        // GET: api/Projects/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectCompactReadDTO>>> GetUserProjects(int userId)
        {
            return _mapper.Map<List<ProjectCompactReadDTO>>(await _projectService.GetUserProjectsAsync(userId));
        }

        /// <summary> Adds a new project entry to the database. </summary>
        /// <param name="dtoProject">
        ///     A creation-specific DTO representing the new project.
        /// </param>
        /// <returns>
        ///     A read-specific DTO of the message just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<ProjectCreateDTO>> PostProject(ProjectCreateDTO dtoProject)
        {
            Project domainProject = _mapper.Map<Project>(dtoProject);
            await _projectService.AddAsync(domainProject, dtoProject.UserIds.ToList(), dtoProject.SkillIds.ToList());

            return CreatedAtAction("GetProject",
                new { projectId = domainProject.Id },
                _mapper.Map<ProjectCompleteReadDTO>(domainProject));
        }

        /// <summary>
        ///     Updates the specified project in the database to match the provided DTO.
        /// </summary>
        /// <param name="projectId"> The id of the project to update. </param>
        /// <param name="dtoProject">
        ///     An edit-specific DTO containing the updated version of the project.
        /// </param>
        /// <returns>
        ///     NoContent on successful database update,
        ///     BadRequest if the provided id and the id of the project do not match,
        ///     or NotFound if the provided id does not match any projects in the database.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     Thrown when the project is found in the database but not able to be updated.
        /// </exception>
        // PUT: api/Projects/5
        [HttpPut("{projectId}")]
        public async Task<IActionResult> PutProject(int projectId, ProjectEditDTO dtoProject)
        {
            if (projectId != dtoProject.Id)
                return BadRequest();

            if (!_projectService.EntityExists(projectId))
                return NotFound();

            var domainProject = _mapper.Map<Project>(dtoProject);

            try
            {
                await _projectService.UpdateAsync(domainProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_projectService.EntityExists(projectId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
