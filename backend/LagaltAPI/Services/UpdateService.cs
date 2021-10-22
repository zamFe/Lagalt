using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class UpdateService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public UpdateService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<Update> AddAsync(Update newUpdate)
        {
            newUpdate.User = await _context.Users.FindAsync(newUpdate.UserId);
            _context.Updates.Add(newUpdate);
            await _context.SaveChangesAsync();
            return newUpdate;
        }

        public async Task<Update> GetByIdAsync(int updateId)
        {
            return await _context.Updates
                .AsNoTracking()
                .Include(application => application.User)
                .Where(application => application.Id == updateId)
                .FirstAsync();
        }

        public async Task<IEnumerable<Update>> GetPageByProjectIdAsync(
            int projectId, PageRange range)
        {
            return await _context.Updates
                .AsNoTracking()
                .Include(update => update.User)
                .Where(update => update.ProjectId == projectId)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .OrderByDescending(application => application.Id)
                .ToListAsync();
        }

        public async Task<int> GetTotalProjectUpdatesAsync(int projectId)
        {
            return await _context.Updates
                .Where(update => update.ProjectId == projectId)
                .CountAsync();
        }
    }
}
