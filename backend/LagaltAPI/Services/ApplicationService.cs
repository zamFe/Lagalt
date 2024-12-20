﻿using LagaltAPI.Context;
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

        public bool ApplicationExists(int applicationId)
        {
            return _context.Applications.Find(applicationId) != null;
        }

        public bool UserHasAppliedToProject(int userId, int applicationId)
        {
            return _context.Applications.Any(application =>
                application.UserId == userId && application.ProjectId == applicationId);
        }

        public async Task<Application> AddAsync(Application newApplication)
        {
            var user = await _context.Users
                .Include(user => user.Skills)
                .Where(user => user.Id == newApplication.UserId)
                .FirstAsync();
            user.AppliedTo = user.AppliedTo.Union(new int[] {newApplication.ProjectId}).ToArray();
            newApplication.User = user;

            _context.Entry(user).State = EntityState.Modified;
            _context.Applications.Add(newApplication);
            await _context.SaveChangesAsync();
            return newApplication;
        }

        public async Task<Application> GetReadonlyByIdAsync(int applicationId)
        {
            return await _context.Applications
                .AsNoTracking()
                .Include(application => application.User.Skills)
                .Where(application => application.Id == applicationId)
                .FirstAsync();
        }

        public async Task<Application> GetWriteableByIdAsync(int applicationId)
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
                .AsNoTracking()
                .Include(application => application.User.Skills)
                .Where(application => application.ProjectId == projectId)
                .Where(application => application.Seen == false)
                .Where(application => application.Accepted == false)
                .OrderByDescending(application => application.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
        }

        public async Task<int> GetTotalProjectApplicationsAsync(int projectId)
        {
            return await _context.Applications
                .Where(application => application.ProjectId == projectId)
                .CountAsync();
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
