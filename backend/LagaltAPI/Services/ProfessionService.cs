using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class ProfessionService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public ProfessionService(LagaltContext context)
        {
            _context = context;
        }

        public bool ProfessionExists(int professionId)
        {
            return _context.Professions.Find(professionId) != null;
        }

        public async Task<IEnumerable<Profession>> GetAllAsync()
        {
            return await _context.Professions
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
