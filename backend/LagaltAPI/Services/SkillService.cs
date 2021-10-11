using LagaltAPI.Context;
using LagaltAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class SkillService : IService<Skill>
    {
        private readonly LagaltContext _context;

        public SkillService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills.Include(s => s.Users).ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int Id)
        {
            return await _context.Skills
                .Include(s => s.Users)
                .Where(s => s.Id == Id)
                .FirstAsync();
        }

        public async Task<Skill> AddAsync(Skill entity)
        {
            _context.Skills.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Skill entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool EntityExists(int id)
        {
            return _context.Skills.Any(s => s.Id == id);
        }
    }
}
