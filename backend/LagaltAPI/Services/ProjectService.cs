using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class ProjectService : IService<Project>
    {

        private readonly LagaltContext _context;

        public ProjectService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Messages)
                .Include(p => p.UserProjects)
                .Include(p => p.Skills)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int Id)
        {
            return await _context.Projects
                .Include(p => p.Messages)
                .Include(p => p.UserProjects)
                .Include(p => p.Skills)
                .Where(p => p.Id == Id)
                .FirstAsync();
        }

        public async Task<Project> AddAsync(Project entity)
        {
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool EntityExists(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }
    }
}
