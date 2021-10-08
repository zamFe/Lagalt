using AutoMapper;
using LagaltAPI.Models.DTOs.Profession;
using LagaltAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProfessionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ProfessionService _service;

        public ProfessionsController(IMapper mapper, ProfessionService service)
        {
            _mapper = mapper;
            _service = service;
        }

        /// <summary> Fetches all available professions from the database. </summary>
        /// <returns> An enumerable containing read-specific DTOs of the professions. </returns>
        // GET: api/Professions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessionReadDTO>>> GetProfessions()
        {
            return _mapper.Map<List<ProfessionReadDTO>>(await _service.GetAllAsync());
        }

        /// <summary> Fetches a profession from the database based on id. </summary>
        /// <param name="id"> The id of the profession to retrieve. </param>
        /// <returns>
        ///     A read-specific DTO of the profession if it is found in the database.
        ///     If it is not, then NotFound is returned instead.
        /// </returns>
        // GET: api/Professions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessionReadDTO>> GetProfession(int id)
        {
            try
            {
                var domainProfession = await _service.GetByIdAsync(id);

                if (domainProfession != null)
                    return _mapper.Map<ProfessionReadDTO>(domainProfession);
                else
                    return NotFound();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /* PUT, POST and DELETE are disabled until support for more professions is added.
         *
         * // PUT: api/Professions/5
         * [HttpPut("{id}")]
         * public async Task<IActionResult> PutProfession(int id, Profession profession)
         * {
         *     if (id != profession.Id)
         *         return BadRequest();
         *
         *     _context.Entry(profession).State = EntityState.Modified;
         *
         *     try
         *         await _context.SaveChangesAsync();
         *     catch (DbUpdateConcurrencyException)
         *     {
         *         if (!ProfessionExists(id))
         *             return NotFound();
         *         else
         *             throw;
         *     }
         *
         *     return NoContent();
         * }
         * 
         * // POST: api/Professions
         * [HttpPost]
         * public async Task<ActionResult<Profession>> PostProfession(Profession profession)
         * {
         *     _context.Professions.Add(profession);
         *     await _context.SaveChangesAsync();
         *
         *     return CreatedAtAction("GetProfession", new { id = profession.Id }, profession);
         * }
         *
         * // DELETE: api/Professions/5
         * [HttpDelete("{id}")]
         * public async Task<IActionResult> DeleteProfession(int id)
         * {
         *     var profession = await _context.Professions.FindAsync(id);
         *    if (profession == null)
         *         return NotFound();
         * 
         *     _context.Professions.Remove(profession);
         *    await _context.SaveChangesAsync();
         * 
         *     return NoContent();
         * }
         */
    }
}
