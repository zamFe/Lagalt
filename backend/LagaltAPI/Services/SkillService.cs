using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class SkillService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public SkillService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<Skill> AddAsync(Skill newSkill)
        {
            _context.Skills.Add(newSkill);
            await _context.SaveChangesAsync();
            return newSkill;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills.Include(skill => skill.Users).ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int skillId)
        {
            return await _context.Skills
                .Include(skill => skill.Users)
                .Where(skill => skill.Id == skillId)
                .FirstAsync();
        }
    }
}
