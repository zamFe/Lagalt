using LagaltAPI.Context;
using LagaltAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Repositories
{
    public class ProfessionService : IService<Profession>
    {
        private readonly LagaltContext _context;

        public ProfessionService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profession>> GetAllAsync()
        {
            return await _context.Professions.Include(p => p.Projects).ToListAsync();
        }

        public async Task<Profession> GetByIdAsync(int id)
        {
            return await _context.Professions
                .Include(p => p.Projects)
                .Where(p => p.Id == id)
                .FirstAsync();
        }

        public async Task<Profession> AddAsync(Profession entity)
        {
            _context.Professions.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var profession = await _context.Professions.FindAsync(id);
            _context.Professions.Remove(profession);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profession entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public bool EntityExists(int id)
        {
            return _context.Professions.Any(p => p.Id == id);
        }
    }
}
