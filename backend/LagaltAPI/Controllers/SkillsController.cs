using AutoMapper;
using LagaltAPI.Context;
using LagaltAPI.Models;
using LagaltAPI.Models.DTOs.Skill;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly LagaltContext _context;
        private readonly IMapper _mapper;

        public SkillsController(LagaltContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillReadDTO>>> GetSkills()
        {
            return _mapper.Map<List<SkillReadDTO>>(await _context.Skills.ToListAsync());
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillReadDTO>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            return _mapper.Map<SkillReadDTO>(skill);
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(int id, SkillEditDTO skillDTO)
        {
            if (id != skillDTO.Id)
                return BadRequest();

            Skill domainSkill = _mapper.Map<Skill>(skillDTO);
            _context.Entry(domainSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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
            Skill skillDomain = _mapper.Map<Skill>(skillDTO);
            _context.Skills.Add(skillDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkill",
                new { id = skillDomain.Id },
                _mapper.Map<SkillReadDTO>(skillDomain));
        }

        /* TODO - Decide whether to support deleting skills
         * 
        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
