using AutoMapper;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Skill;
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
    public class SkillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SkillService _service;

        // Constructor.
        public SkillsController(IMapper mapper, SkillService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary> Fetches all available skills from the database. </summary>
        /// <returns> An enumerable containing read-specific DTOs of the skills. </returns>
        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillReadDTO>>> GetSkills()
        {
            return _mapper.Map<List<SkillReadDTO>>(await _service.GetAllAsync());
        }

        /// <summary> Fetches a skill from the database based on id. </summary>
        /// <param name="id"> The id of the skill to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the skill if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillReadDTO>> GetSkill(int id)
        {
            try
            {
                var domainSkill = await _service.GetByIdAsync(id);

                if (domainSkill != null)
                    return _mapper.Map<SkillReadDTO>(domainSkill);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///     Updates the specified skill in the database to match the provided DTO.
        /// </summary>
        /// <param name="id"> The id of the skill to replace. </param>
        /// <param name="dtoSkill">
        ///     An edit-specific DTO containing the updated version of the skill.
        /// </param>
        /// <returns>
        ///     NoContent on successful database update,
        ///     BadRequest if the provided id and the id of the skill do not match,
        ///     or NotFound if the provided id does not match any skills in the database.
        /// </returns>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     Thrown when the skill is found in the database but not able to be updated.
        /// </exception>
        // PUT: api/Skills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillEditDTO dtoSkill)
        {
            if (id != dtoSkill.Id)
                return BadRequest();

            if (!_service.EntityExists(id))
                return NotFound();

            var domainSkill = _mapper.Map<Skill>(dtoSkill);

            try
            {
                await _service.UpdateAsync(domainSkill);
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

        /// <summary> Adds a new skill entry to the database. </summary>
        /// <param name="dtoSkill"> A creation-specific DTO representing the new skill. </param>
        /// <returns>
        ///     A read-specific DTO of the skill just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Skills
        [HttpPost]
        public async Task<ActionResult<SkillCreateDTO>> PostSkill(SkillCreateDTO dtoSkill)
        {
            var domainSkill = _mapper.Map<Skill>(dtoSkill);
            await _service.AddAsync(domainSkill);

            return CreatedAtAction("GetUser",
                new { id = domainSkill.Id },
                _mapper.Map<SkillReadDTO>(domainSkill));
        }

        /* TODO - Decide whether or not deleting skills will be supported.
         * 
         * // DELETE: api/Skills/5
         * [HttpDelete("{id}")]
         * public async Task<IActionResult> DeleteSkill(int id)
         * {
         *    var skill = await _context.Skills.FindAsync(id);
         *    if (skill == null)
         *    {
         *        return NotFound();
         *    }
         *
         *    _context.Skills.Remove(skill);
         *    await _context.SaveChangesAsync();
         * 
         *   return NoContent();
         * }
         */
    }
}
