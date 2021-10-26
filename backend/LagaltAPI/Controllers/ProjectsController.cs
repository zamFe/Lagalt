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
        private readonly ProfessionService _professionService;
        private readonly ProjectService _projectService;
        private readonly SkillService _skillService;
        private readonly UriService _uriService;
        private readonly UserService _userService;

        // Constructor.
        public ProjectsController(
            IMapper mapper, ProfessionService professionService, ProjectService projectService,
            SkillService skillService, UriService uriService, UserService userService)
        {
            _mapper = mapper;
            _professionService = professionService;
            _projectService = projectService;
            _skillService = skillService;
            _uriService = uriService;
            _userService = userService;
        }

        /// <summary> Checks whether a creation-specific DTO is properly formated. </summary>
        /// <param name="dtoProject"> A creation-specific DTO containing a new project. </param>
        /// <returns> A ValidationResult with a result and the reason for the result. </returns>
        private ValidationResult ValidateNewProject(ProjectCreateDTO dtoProject)
        {
            if (dtoProject.AdministratorIds.Length == 0 || dtoProject.Users.Length == 0)
                return new ValidationResult(false, "A project must have users and administrators");

            foreach (int adminId in dtoProject.AdministratorIds)
            {
                if (!Array.Exists(dtoProject.Users, userId => userId == adminId))
                {
                    return new ValidationResult(
                        false, "Project administrators must be project members");
                }
            }

            if (!_professionService.ProfessionExists(dtoProject.ProfessionId))
                return new ValidationResult(false, "Project has invalid profession id");

            foreach (int skillId in dtoProject.Skills)
            {
                if (!_skillService.SkillExists(skillId))
                    return new ValidationResult(false, "Project has invalid skill ids");
            }

            foreach (int userId in dtoProject.Users)
            {
                if (!_userService.UserExists(userId))
                    return new ValidationResult(false, "Project has invalid user ids");
            }

            foreach (int adminId in dtoProject.AdministratorIds)
            {
                if (!_userService.UserExists(adminId))
                    return new ValidationResult(false, "Project has invalid admin ids");
            }

            return new ValidationResult(true);
        }

        /// <summary> Checks whether an update-specific DTO is properly formated. </summary>
        /// <param name="dtoProject">
        ///     An edit-specific DTO containing an updated version of a project.
        /// </param>
        /// <param name="endpoint"> The endpoint at which the PUT request was recieved. </param>
        /// <returns> A ValidationResult with a result and the reason for the result. </returns>
        private ValidationResult ValidateUpdatedProject(ProjectEditDTO dtoProject, int endpoint)
        {
            if (endpoint != dtoProject.Id)
                return new ValidationResult(false, "Mismatch between project id and API endpoint");

            foreach (int skillId in dtoProject.Skills)
            {
                if (!_skillService.SkillExists(skillId))
                    return new ValidationResult(false, "Project has invalid skill ids");
            }

            return new ValidationResult(true);
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
        ///     Fetches projects from the database, filtering based on query parameters.
        /// </summary>
        /// <param name="offset"> Specifies the index of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to include. </param>
        /// <param name="professionId"> Specifies a profession id to filter projects by. </param>
        /// <param name="keyword"> Specifies a keyword to filter projects by. </param>
        /// <returns>
        ///     A page containing all available read-specific DTOs within the specified range.
        /// </returns>
        // GET api/Projects?offset=5&limit=5
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Page<ProjectCompactReadDTO>>> GetProjects(
            [FromQuery] int offset, [FromQuery] int limit, [FromQuery] int professionId,
            [FromQuery] string keyword)
        {
            // TODO - Track history (viewed).

            var range = new PageRange(offset, limit);
            var validKeyword = keyword == null ? "" : keyword.Trim().ToLower();

            var projects = _mapper.Map<List<ProjectCompactReadDTO>>(
                await _projectService.GetPageAsync(range, professionId, validKeyword));
            var totalProjects = await _projectService.GetTotalProjectsAsync(professionId, validKeyword);
            var baseUri = _uriService.GetBaseUrl() + "api/Projects";
            return new Page<ProjectCompactReadDTO>(projects, totalProjects, range, baseUri, validKeyword);
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
            var validation = ValidateNewProject(dtoProject);
            if (!validation.Result)
                return BadRequest(validation.RejectionReason);

            var domainProject = _mapper.Map<Project>(dtoProject);
                    
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
            var validation = ValidateUpdatedProject(dtoProject, projectId);
            if (!validation.Result)
                return BadRequest(validation.RejectionReason);

            if (!_projectService.ProjectExists(projectId))
                return NotFound();

            var domainProject = await _projectService.GetWriteableByIdAsync(projectId);
            _mapper.Map<ProjectEditDTO, Project>(dtoProject, domainProject);
            try
            {
                await _projectService.UpdateAsync(domainProject, dtoProject.Skills);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_projectService.ProjectExists(projectId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
