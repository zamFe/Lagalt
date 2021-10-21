using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Skill;
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

        /// <summary> Fetches a skill from the database based on skill id. </summary>
        /// <param name="skillId"> The id of the skill to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the skill if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Skills/Id/5
        [HttpGet("Id/{skillId}")]
        public async Task<ActionResult<SkillReadDTO>> GetSkillById(int skillId)
        {
            try
            {
                var domainSkill = await _service.GetByIdAsync(skillId);

                if (domainSkill != null)
                    return _mapper.Map<SkillReadDTO>(domainSkill);
            }
            catch (InvalidOperationException) {}
            return NotFound();
        }

        /// <summary> Fetches a skill from the database based on skill name. </summary>
        /// <param name="skillName"> The name of the skill to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the skill if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Skills/Name/Python
        [HttpGet("Name/{skillName}")]
        public async Task<ActionResult<SkillReadDTO>> GetSkillByName(string skillName)
        {
            try
            {
                var domainSkill = await _service.GetByNameAsync(skillName);

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

        /// <summary> Adds a new skill entry to the database. </summary>
        /// <param name="dtoSkill"> A creation-specific DTO representing the new skill. </param>
        /// <returns>
        ///     A read-specific DTO of the skill just added to the database on success,
        ///     or BadRequest on failure.
        /// </returns>
        // POST: api/Skills
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SkillReadDTO>> PostSkill(SkillCreateDTO dtoSkill)
        {
            if (_service.SkillNameExists(dtoSkill.Name))
                return BadRequest("Skill name already exists");

            var domainSkill = _mapper.Map<Skill>(dtoSkill);
            await _service.AddAsync(domainSkill, dtoSkill.Users, dtoSkill.Projects);

            return CreatedAtAction("GetSkillById",
                new { skillId = domainSkill.Id },
                _mapper.Map<SkillReadDTO>(domainSkill));
        }
    }
}
