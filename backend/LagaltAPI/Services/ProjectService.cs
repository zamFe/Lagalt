using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class ProjectService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public ProjectService(LagaltContext context)
        {
            _context = context;
        }

        public bool EntityExists(int projectId)
        {
            return _context.Projects.Any(project => project.Id == projectId);
        }

        public async Task<Project> AddAsync(
            Project newProject, IEnumerable<int> userIds, IEnumerable<int> skillIds)
        {
            newProject.Users = await _context.Users
                .Where(user => userIds.Any(userId => userId == user.Id))
                .ToListAsync();

            newProject.Skills = await _context.Skills
                .Where(skill => skillIds.Any(skillId => skillId == skill.Id))
                .ToListAsync();

            newProject.Profession = await _context.Professions
                .FindAsync(newProject.ProfessionId);

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
            return newProject;
        }

        public async Task<IEnumerable<Project>> GetPageAsync(PageRange range)
        {
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .OrderByDescending(project => project.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetRecommendedProjectsPageAsync(
            int userId, PageRange range)
        {
            var userSkillIds = await _context.Skills
                .Where(skill => skill.Users.Any(user => user.Id == userId))
                .Select(skill => skill.Id)
                .ToListAsync();

            // TODO - Include projects joined by fellow contributors.
            //        Currently only looks at projects matching the user's skills.
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => !project.Users.Any(projectUser =>
                    projectUser.Id == userId))
                .Where(project => project.Skills.Any(projectSkill =>
                    userSkillIds.Contains(projectSkill.Id)))
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .OrderByDescending(project => project.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetUserProjectsPageAsync(
            int userId, PageRange range)
        {
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Users.Any(user => user.Id == userId))
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .OrderByDescending(project => project.Id)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Id == projectId)
                .FirstAsync();
        }

        public async Task UpdateAsync(Project updatedProject, IEnumerable<int> skillIds)
        {
            updatedProject.Skills = await _context.Skills
                .Where(skill => skillIds.Any(skillId => skillId == skill.Id))
                .ToListAsync();

            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
