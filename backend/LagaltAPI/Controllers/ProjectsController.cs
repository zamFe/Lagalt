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
        private readonly ProjectService _service;

        // Constructor.
        public ProjectsController(IMapper mapper, ProjectService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        ///     Fetches projects from the database according to the specified offset and limit.
        /// </summary>
        /// <param name="offset"> Specifies the id of the first project to be included. </param>
        /// <param name="limit"> Specifies how many projects to fetch. </param>
        /// <returns>
        ///     A page containing all available read-specific DTOs within the specified range.
        /// </returns>
        // GET api/Projects?offset=5&limit=5
        [HttpGet]
        public async Task<ActionResult<Page<ProjectReadDTO>>> GetProjects(
            [FromQuery] int offset, [FromQuery] int limit)
        {
            var validOffset = offset < 1 ? 1 : offset;
            var validLimit = limit < 1 || limit > 10 ? 10 : limit;

            var projects = _mapper.Map<List<ProjectReadDTO>>(
                await _service.GetOffsetPageAsync(
                    startId: validOffset,
                    pageSize: validLimit
                ));
            return new Page<ProjectReadDTO>(projects);
        }

        /// <summary> Fetches a project from the database based on id. </summary>
        /// <param name="id"> The id of the project to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the project if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDTO>> GetProject(int id)
        {
            try
            {
                var domainProject = await _service.GetByIdAsync(id);

                if (domainProject != null)
                    return _mapper.Map<ProjectReadDTO>(domainProject);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Updates the specified project in the database to match the provided DTO.
        /// </summary>
        /// <param name="id"> The id of the project to update. </param>
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectEditDTO dtoProject)
        {
            if (id != dtoProject.Id)
                return BadRequest();

            if (!_service.EntityExists(id))
                return NotFound();

            var domainProject = _mapper.Map<Project>(dtoProject);

            try
            {
                await _service.UpdateAsync(domainProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.EntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
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
            await _service.AddAsync(domainProject, dtoProject.Users.ToList(), dtoProject.Skills.ToList());

            return CreatedAtAction("GetProject", 
                new { id = domainProject.Id }, 
                _mapper.Map<ProjectReadDTO>(domainProject));
        }
    }
}
