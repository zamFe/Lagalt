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

        public async Task<Skill> AddAsync(Skill newSkill, List<int> UserIds, List<int> ProjectIds)
        {
            List<User> users = await _context.Users
                .Where(u => UserIds.Any(id => id == u.Id))
                .ToListAsync();
            List<Project> projects = await _context.Projects
                .Where(p => ProjectIds.Any(id => id == p.Id))
                .ToListAsync();

            newSkill.Users = users;
            newSkill.Projects = projects;

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
                .Include(skill => skill.Projects)
                .Where(skill => skill.Id == skillId)
                .FirstAsync();
        }
    }
}
