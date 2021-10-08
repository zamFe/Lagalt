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
    public class SkillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SkillService _service;

        public SkillsController(IMapper mapper, SkillService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillReadDTO>>> GetSkills()
        {
            return _mapper.Map<List<SkillReadDTO>>(await _service.GetAllAsync());
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillReadDTO>> GetSkill(int id)
        {
            try
            {
                var skillDomain = await _service.GetByIdAsync(id);

                if (skillDomain != null)
                    return _mapper.Map<SkillReadDTO>(skillDomain);
                else
                    return NotFound();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillEditDTO skillDTO)
        {
            if (id != skillDTO.Id)
                return BadRequest();

            if (!_service.EntityExists(id))
                return NotFound();

            var skillDomain = _mapper.Map<Skill>(skillDTO);

            try
            {
                await _service.UpdateAsync(skillDomain);
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

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SkillCreateDTO>> PostSkill(SkillCreateDTO skillDTO)
        {
            var skillDomain = _mapper.Map<Skill>(skillDTO);
            await _service.AddAsync(skillDomain);

            return CreatedAtAction("GetUser",
                new { id = skillDomain.Id },
                _mapper.Map<SkillReadDTO>(skillDomain));
        }

        /* TODO - Decide whether to support deleting skills
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
