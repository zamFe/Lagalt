using AutoMapper;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.DTOs.Skill;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // TODO - Add GetSkill for returned result from post method.


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
            await _service.AddAsync(domainSkill);

            return CreatedAtAction("GetSkill",
                new { id = domainSkill.Id },
                _mapper.Map<SkillReadDTO>(domainSkill));
        }
    }
}
