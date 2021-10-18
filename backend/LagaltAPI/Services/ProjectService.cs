using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
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

        public async Task<Project> AddAsync(Project newProject, List<int> UserIds, List<int> SkillIds)
        {
            List<User> users = await _context.Users
                .Where(u => UserIds.Any(id => id == u.Id))
                .ToListAsync();
            List<Skill> skills = await _context.Skills
                .Where(s => SkillIds.Any(id => id == s.Id))
                .ToListAsync();

            newProject.Users = users;
            newProject.Skills = skills;

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
            return newProject;
        }

        public async Task<IEnumerable<Project>> GetOffsetPageAsync(int startId, int pageSize)
        {
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Skip(startId - 1)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetRecommendedProjectsAsync(int userId)
        {
            var user = await _context.Users
                .Include(user => user.Skills)
                .Include(user => user.Projects)
                .Where(user => user.Id == userId)
                .FirstAsync();

            // Todo - Include projects joined by fellow contributors.
            //        Currently only looks at projects matching the user's skills.
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project =>
                    !project.Users.Any(user => user.Id == userId
                    && project.Skills.Any(skill => user.Skills.Contains(skill))))
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetUserProjectsAsync(int userId)
        {
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Users.Any(user => user.Id == userId))
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

        public async Task UpdateAsync(Project updatedProject)
        {
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
