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

        public bool SkillExists(int skillId)
        {
            return _context.Skills.Find(skillId) != null;
        }

        public bool SkillNameExists(string skillName)
        {
            var normalizedSkillName = skillName.Trim().ToLower();
            return _context.Skills
                .Any(skill =>skill.Name.Trim().ToLower() == normalizedSkillName);
        }

        public async Task<Skill> AddAsync(
            Skill newSkill, IEnumerable<int> userIds, IEnumerable<int> projectIds)
        {
            newSkill.Users = await _context.Users
                .Where(user => userIds.Any(userId => userId == user.Id))
                .ToListAsync();
            newSkill.Projects = await _context.Projects
                .Where(project => projectIds.Any(projectId => projectId == project.Id))
                .ToListAsync();
            newSkill.Name = newSkill.Name.Trim();

            _context.Skills.Add(newSkill);
            await _context.SaveChangesAsync();
            return newSkill;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int skillId)
        {
            return await _context.Skills
                .AsNoTracking()
                .Where(skill => skill.Id == skillId)
                .FirstAsync();
        }

        public async Task<Skill> GetByNameAsync(string skillName)
        {
            var normalizedSkillName = skillName.Trim().ToLower();
            return await _context.Skills
                .AsNoTracking()
                .Where(skill => skill.Name.ToLower() == normalizedSkillName)
                .FirstAsync();
        }
    }
}
