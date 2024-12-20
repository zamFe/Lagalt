﻿using LagaltAPI.Context;
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

        public bool ProjectExists(int projectId)
        {
            return _context.Projects.Find(projectId) != null;
        }

        public bool UserIsProjectMember(int projectId, int userId)
        {
            return _context.Projects
                .Where(project => project.Id == projectId)
                .Any(project => project.Users.Any(user => user.Id == userId));
        }

        public bool UserIsProjectAdministrator(int projectId, int userId)
        {
            return _context.Projects
                .Where(project => project.Id == projectId)
                .Any(project => project.AdministratorIds.Contains(userId));
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

        public async Task<Project> GetReadonlyByIdAsync(int projectId)
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Id == projectId)
                .FirstAsync();
        }

        public async Task<Project> GetWriteableByIdAsync(int projectId)
        {
            return await _context.Projects
                .Include(project => project.Messages)
                .Include(project => project.Users)
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Id == projectId)
                .FirstAsync();
        }

        public async Task<IEnumerable<Project>> GetPageAsync(
            PageRange range, int professionId = 0, string keyword = "")
        {
            var normalizedKeyword = keyword == null ? "" : keyword.ToLower().Trim();

            if (professionId == 0 && normalizedKeyword == "")
            {
                // No filters.
                return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .OrderByDescending(project => project.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
            }
            else if (professionId != 0 && normalizedKeyword == "")
            {
                // Just profession filter.
                return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.ProfessionId == professionId)
                .OrderByDescending(project => project.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
            }
            else if (professionId == 0 && normalizedKeyword != "")
            {
                // Just keyword filter.
                return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Title.ToLower().Contains(normalizedKeyword))
                .OrderByDescending(project => project.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
            }
            else
            {
                // Both profession & keyword filter.
                return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.ProfessionId == professionId)
                .Where(project => project.Title.ToLower().Contains(normalizedKeyword))
                .OrderByDescending(project => project.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
            }            
        }

        // Currently only looks at projects matching the user's skills.
        // Other factors could be popularity (general and among fellow project members),
        // surges in activity, how recent the project is, etc.
        public async Task<IEnumerable<Project>> GetRecommendedProjectsPageAsync(
            int userId, PageRange range)
        {
            var userSkillIds = await _context.Skills
                .AsNoTracking()
                .Where(skill => skill.Users.Any(user => user.Id == userId))
                .Select(skill => skill.Id)
                .ToListAsync();

            return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => !project.Users.Any(projectUser =>
                    projectUser.Id == userId))
                .Where(project => project.Skills.Any(projectSkill =>
                    userSkillIds.Contains(projectSkill.Id)))
                .OrderByDescending(project => project.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetUserProjectsPageAsync(
            int userId, PageRange range)
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(project => project.Skills)
                .Include(project => project.Profession)
                .Where(project => project.Users.Any(user => user.Id == userId))
                .OrderByDescending(project => project.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
        }

        public async Task<int> GetTotalProjectsAsync(int professionId = 0, string keyword = "")
        {
            var normalizedKeyword = keyword == null ? "" : keyword.ToLower().Trim();

            if (professionId == 0 && normalizedKeyword == "")
            {
                // No filters.
                return await _context.Projects.CountAsync();
            }
            else if (professionId != 0 && normalizedKeyword == "")
            {
                // Just profession filter.
                return await _context.Projects
                .Where(project => project.ProfessionId == professionId)
                .CountAsync();
            }
            else if (professionId == 0 && normalizedKeyword != "")
            {
                // Just keyword filter.
                return await _context.Projects
                .Where(project => project.Title.ToLower().Contains(normalizedKeyword))
                .CountAsync();
            }
            else
            {
                // Both profession & keyword filter.
                return await _context.Projects
                .Where(project => project.ProfessionId == professionId)
                .Where(project => project.Title.ToLower().Contains(normalizedKeyword))
                .CountAsync();
            }
        }

        public async Task<int> GetTotalRecommendedProjectsAsync(int userId)
        {
            var userSkillIds = await _context.Skills
                .AsNoTracking()
                .Where(skill => skill.Users.Any(user => user.Id == userId))
                .Select(skill => skill.Id)
                .ToListAsync();

            return await _context.Projects
                .Where(project => !project.Users.Any(projectUser =>
                    projectUser.Id == userId))
                .Where(project => project.Skills.Any(projectSkill =>
                    userSkillIds.Contains(projectSkill.Id)))
                .CountAsync();
        }

        public async Task<int> GetTotalUserProjectsAsync(int userId)
        {
            return await _context.Projects
                .Where(project => project.Users.Any(user => user.Id == userId))
                .CountAsync();
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
