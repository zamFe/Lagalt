using AutoMapper;
using LagaltAPI.Models.DTOs.Profession;
using LagaltAPI.Services;
using Microsoft.AspNetCore.Mvc;
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

        // Constructor.
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
    }
}
