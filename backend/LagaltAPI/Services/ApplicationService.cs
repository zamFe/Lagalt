using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class ApplicationService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public ApplicationService(LagaltContext context)
        {
            _context = context;
        }
        public async Task<Application> AddAsync(Application newApplication)
        {
            _context.Applications.Add(newApplication);
            await _context.SaveChangesAsync();
            newApplication.User = await _context.Users.FindAsync(newApplication.UserId);
            return newApplication;
        }

        public async Task<Application> GetByIdAsync(int applicationId)
        {
            return await _context.Applications
                .Include(application => application.User)
                .Where(application => application.Id == applicationId)
                .FirstAsync();
        }
        public async Task<IEnumerable<Application>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Applications
                .Include(application => application.User)
                .Where(application => application.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
