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

        public async Task<Project> AddAsync(Project newProject)
        {
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
