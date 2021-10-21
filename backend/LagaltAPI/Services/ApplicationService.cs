using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.Wrappers;
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

        public bool EntityExists(int applicationId)
        {
            return _context.Applications.Any(application => application.Id == applicationId);
        }

        public bool UserHasAppliedToProject(int userId, int projectId)
        {
            return _context.Applications.Any(application =>
                application.UserId == userId && application.ProjectId == projectId);
        }

        public async Task<Application> AddAsync(Application newApplication)
        {
            // TODO - Check if skills are missing from returned application.
            // TODO - Uncomment when ready to log history for applications.
            var user = await _context.Users
                .FindAsync(newApplication.UserId);
            //user.AppliedTo.Add(newApplication.Id);
            newApplication.User = user;

            //_context.Entry(user).State = EntityState.Modified;
            _context.Applications.Add(newApplication);

            await _context.SaveChangesAsync();
            return newApplication;
        }

        public async Task<Application> GetByIdAsync(int applicationId)
        {
            return await _context.Applications
                .Include(application => application.User.Skills)
                .Where(application => application.Id == applicationId)
                .FirstAsync();
        }

        public async Task<IEnumerable<Application>> GetPageByProjectIdAsync(
            int projectId, PageRange range)
        {
            return await _context.Applications
                .Include(application => application.User.Skills)
                .Where(application => application.ProjectId == projectId)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .OrderByDescending(application => application.Id)
                .ToListAsync();
        }

        public async Task UpdateAsync(Application updatedApplication, bool newlyAccepted = false)
        {
            if (newlyAccepted)
            {
                updatedApplication.Seen = true;

                var user = await _context.Users
                    .Include(user => user.Projects)
                    .FirstAsync(user => user.Id == updatedApplication.UserId);
                var project = await _context.Projects.FindAsync(updatedApplication.ProjectId);
                user.Projects.Add(project);
                _context.Entry(user).State = EntityState.Modified;
            }

            _context.Entry(updatedApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
