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
using LagaltAPI.Models.DTOs.Profession;
using LagaltAPI.Repositories;

namespace LagaltAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ProfessionService _service;

        public ProfessionsController(IMapper mapper, ProfessionService service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/Professions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessionReadDTO>>> GetProfessions()
        {
            return _mapper.Map<List<ProfessionReadDTO>>(await _service.GetAllAsync());
        }

        // GET: api/Professions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessionReadDTO>> GetProfession(int id)
        {
            try
            {
                var professionDomain = await _service.GetByIdAsync(id);

                if (professionDomain != null)
                    return _mapper.Map<ProfessionReadDTO>(professionDomain);
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

        /* PUT, POST and DELETE are disabled until support for more professions is added
         *
        * // PUT: api/Professions/5
        * // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        * // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
