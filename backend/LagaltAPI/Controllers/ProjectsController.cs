using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LagaltAPI.Context;
using LagaltAPI.Models;
using AutoMapper;
using LagaltAPI.Models.DTOs.Project;
using LagaltAPI.Repositories;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _service;
        private readonly IMapper _mapper;

        public ProjectsController(ProjectService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetProjects()
        {
            return _mapper.Map<List<ProjectReadDTO>>(await _service.GetAllAsync());
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDTO>> GetProject(int id)
        {
            try
            {
                var project = await _service.GetByIdAsync(id);

                if (project != null)
                    return _mapper.Map<ProjectReadDTO>(project);
                else
                    return NotFound();
            }
            catch (ArgumentNullException) { return BadRequest(); }
            catch (InvalidOperationException) { return NotFound(); }
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectEditDTO project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            if (!_service.EntityExists(id))
            {
                return NotFound();
            }

            Project domainProject = _mapper.Map<Project>(project);

            try
            {
                await _service.UpdateAsync(domainProject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectCreateDTO>> PostProject(ProjectCreateDTO dtoProject)
        {
            Project domainProject = _mapper.Map<Project>(dtoProject);
            await _service.AddAsync(domainProject);

            return CreatedAtAction("GetProject", 
                new { id = domainProject.Id }, 
                _mapper.Map<ProjectReadDTO>(domainProject));
        }

        /* NOT CURRENTLY SUPPORTED */
        /*
        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
    }
}
