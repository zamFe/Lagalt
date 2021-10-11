﻿using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Project;
using LagaltAPI.Services;
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
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ProjectService _service;

        public ProjectsController(IMapper mapper, ProjectService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary> Fetches all available projects from the database. </summary>
        /// <returns> An enumerable containing read-specific DTOs of the projects. </returns>
        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetProjects()
        {
            return _mapper.Map<List<ProjectReadDTO>>(await _service.GetAllAsync());
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


        // TODO - Add support for getting a range of projects (offset + limit).
        //        Would this replace GetProjects?


        /// <summary> Replaces the specified project in the database with the provided DTO. </summary>
        /// <param name="id"> The id of the project to replace. </param>
        /// <param name="project"> A edit-specific DTO of the new version of the project. </param>
        /// <returns>
        ///     NoContent on successful deletion,
        ///     BadRequest if the provided id and the id of the project do not match,
        ///     or NotFound if the provided id does not match any projects in the database.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     Thrown when the project is found in the database but not able to be deleted.
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
        /// <param name="dtoMessage"> A create-specific DTO representing the new project. </param>
        /// <returns>
        ///     A read-specific DTO of the message just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<ProjectCreateDTO>> PostProject(ProjectCreateDTO dtoProject)
        {
            var domainProject = _mapper.Map<Project>(dtoProject);
            await _service.AddAsync(domainProject);

            return CreatedAtAction("GetProject", 
                new { id = domainProject.Id }, 
                _mapper.Map<ProjectReadDTO>(domainProject));
        }

        /* TODO - Decide whether or not deleting projects will be supported.
         *
         * // DELETE: api/Projects/5
         * [HttpDelete("{id}")]
         * public async Task<IActionResult> DeleteProject(int id)
         * {
         *     var project = await _context.Projects.FindAsync(id);
         *     if (project == null)
         *         return NotFound();
         * 
         *     _context.Projects.Remove(project);
         *     await _context.SaveChangesAsync();
         * 
         *     return NoContent();
         * }
         */
    }
}