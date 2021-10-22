using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Update;
using LagaltAPI.Models.Wrappers;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UpdatesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UpdateService _updateService;
        private readonly UriService _uriService;

        // Constructor.
        public UpdatesController(
            IMapper mapper, UpdateService updateService, UriService uriService)
        {
            _mapper = mapper;
            _updateService = updateService;
            _uriService = uriService;
        }

        /// <summary> Fetches a project update from the database based on update id. </summary>
        /// <param name="updateId"> The id of the project update to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the project update if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Updates/5
        [HttpGet("{updateId}")]
        public async Task<ActionResult<UpdateReadDTO>> GetUpdate(int updateId)
        {
            try
            {
                var domainUpdate = await _updateService.GetByIdAsync(updateId);

                if (domainUpdate != null)
                    return _mapper.Map<UpdateReadDTO>(domainUpdate);
            }
            catch (InvalidOperationException) {}
            return NotFound();
        }

        /// <summary>
        ///     Fetches project updates from the database based on project id,
        ///     according to the specified offset and limit.
        /// </summary>
        /// <param name="projectId"> The id of the project to retrieve updates from. </param>
        /// <param name="offset">
        ///     The database index (starting at 1) of the first project update to be included.
        /// </param>
        /// <param name="limit"> Specifies how many project updates to include. </param>
        /// <returns>
        ///     An enumerable containing read-specific DTOs of the project updates.
        ///     </returns>
        // GET: api/Updates/Project/5?offset=5&limit=5
        [HttpGet("Project/{projectId}")]
        public async Task<ActionResult<Page<UpdateReadDTO>>> GetProjectUpdates(
            int projectId, [FromQuery] int offset, [FromQuery] int limit)
        {
            var filter = new PageRange(offset, limit);
            var updates = _mapper.Map<List<UpdateReadDTO>>(
                await _updateService.GetPageByProjectIdAsync(projectId, filter));
            var totalUpdates = await _updateService.GetTotalProjectUpdatesAsync(projectId);
            var baseUri = _uriService.GetBaseUrl() + $"api/Applications/Project/{projectId}";
            return new Page<UpdateReadDTO>(updates, totalUpdates, filter, baseUri);
        }

        /// <summary> Adds a new project update entry to the database. </summary>
        /// <param name="dtoUpdate">
        ///     A creation-specific DTO representing the new project update.
        /// </param>
        /// <returns>
        ///     A read-specific DTO of the project update just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Updates
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UpdateReadDTO>> PostUpdate(UpdateCreateDTO dtoUpdate)
        {
            var domainUpdate = _mapper.Map<Update>(dtoUpdate);
            domainUpdate = await _updateService.AddAsync(domainUpdate);

            return CreatedAtAction("GetUpdate",
                new { updateId = domainUpdate.Id },
                _mapper.Map<UpdateReadDTO>(domainUpdate));
        }
    }
}
