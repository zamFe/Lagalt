using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Project;
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
        [AllowAnonymous]
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectCompleteReadDTO>> GetProject(int projectId)
        {
            // TODO - Track history (click).

            try
            {
                var domainProject = await _projectService.GetReadonlyByIdAsync(projectId);

                if (domainProject != null)
                    return _mapper.Map<ProjectCompleteReadDTO>(domainProject);
            }
            catch (InvalidOperationException) {}
            return NotFound();
        }

        /// <summary>
        ///     Fetches projects from the database according to the specified offset and limit.
        /// </summary>
        /// <param name="offset"> Specifies the index of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to include. </param>
        /// <param name="professionId"> Specifies a profession id to filter projects by. </param>
        /// <returns>
        ///     A page containing all available read-specific DTOs within the specified range.
        /// </returns>
        // GET api/Projects?offset=5&limit=5
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Page<ProjectCompactReadDTO>>> GetProjects(
            [FromQuery] int offset, [FromQuery] int limit, [FromQuery] int professionId)
        {
            // TODO - Track history (viewed).

            var range = new PageRange(offset, limit);

            var projects = _mapper.Map<List<ProjectCompactReadDTO>>(
                await _projectService.GetPageAsync(range, professionId));
            var totalProjects = await _projectService.GetTotalProjectsAsync(professionId);
            var baseUri = _uriService.GetBaseUrl() + "api/Projects";
            return new Page<ProjectCompactReadDTO>(projects, totalProjects, range, baseUri);
        }

        /// <summary> Generates recommended projects for a user. </summary>
        /// <param name="userId"> The id of the user to get recommended projects for. </param>
        /// <param name="offset"> Specifies the index of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to include. </param>
        /// <returns>
        ///     A page containing all available read-specific DTOs for the user.
        /// </returns>
        // GET: api/Projects/Recommended/5
        [HttpGet("Recommended/{userId}")]
        public async Task<ActionResult<Page<ProjectCompactReadDTO>>> GetRecommendedProjects(
            int userId, [FromQuery] int offset, [FromQuery] int limit)
        {
            // TODO - Track history (viewed).

            var range = new PageRange(offset, limit);
            var projects = _mapper.Map<List<ProjectCompactReadDTO>>(
                await _projectService.GetRecommendedProjectsPageAsync(userId, range));
            var totalProjects = await _projectService.GetTotalRecommendedProjectsAsync(userId);
            var baseUri = _uriService.GetBaseUrl() + $"api/Projects/Recommended/{userId}";
            return new Page<ProjectCompactReadDTO>(projects, totalProjects, range, baseUri);
        }

        /// <summary> Fetches a user's projects from the database. </summary>
        /// <param name="userId"> The id of the user to retrieve projects for. </param>
        /// <param name="offset"> Specifies the index of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to include. </param>
        /// <returns>
        ///     An enumerable containing read-specific DTOs of the projects joined by the user.
        /// </returns>
        // GET: api/Projects/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<Page<ProjectCompactReadDTO>>> GetUserProjects(
            int userId, [FromQuery] int offset, [FromQuery] int limit)
        {
            var range = new PageRange(offset, limit);
            var projects = _mapper.Map<List<ProjectCompactReadDTO>>(
                await _projectService.GetUserProjectsPageAsync(userId, range));
            var totalProjects = await _projectService.GetTotalUserProjectsAsync(userId);
            var baseUri = _uriService.GetBaseUrl() + $"api/Projects/User/{userId}";
            return new Page<ProjectCompactReadDTO>(projects, totalProjects, range, baseUri);
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
        public async Task<ActionResult<ProjectCompleteReadDTO>> PostProject(
            ProjectCreateDTO dtoProject)
        {
            var domainProject = _mapper.Map<Project>(dtoProject);

            if (dtoProject.AdministratorIds.Length == 0 || dtoProject.Users.Length == 0)
                return BadRequest("A project must have users and administrators");

            foreach (int adminId in dtoProject.AdministratorIds)
            {
                if (!Array.Exists(dtoProject.Users, userId => userId == adminId))
                    return BadRequest("Project administrators must be project members");
            }

            await _projectService.AddAsync(domainProject, dtoProject.Users, dtoProject.Skills);
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

            var domainProject = await _projectService.GetWriteableByIdAsync(projectId);
            _mapper.Map<ProjectEditDTO, Project>(dtoProject, domainProject);

            try
            {
                await _projectService.UpdateAsync(domainProject, dtoProject.Skills);
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
