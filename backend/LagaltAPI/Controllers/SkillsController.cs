using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Skill;
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


        // TODO - Add support for getting a range of skills (offset + limit).
        //        Would this replace GetSkills?


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
            await _service.AddAsync(domainSkill, dtoSkill.Users.ToList(), dtoSkill.Projects.ToList());

            return CreatedAtAction("GetSkill",
                new { id = domainSkill.Id },
                _mapper.Map<SkillReadDTO>(domainSkill));
        }
    }
}
