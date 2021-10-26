using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class UserService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public UserService(LagaltContext context)
        {
            _context = context;
        }

        private async Task<User> HandleUserPrivacy(User user)
        {
            if (false) //user.Hidden)
            {
                user.Description = null;
                user.Portfolio = null;
            }
            else
            {
                user.Skills = await _context.Skills
                    .AsNoTracking()
                    .Where(skill => skill.Users.Any(skillUser =>
                        skillUser.Id == user.Id))
                    .ToListAsync();
                user.Projects = await _context.Projects
                    .AsNoTracking()
                    .Include(project => project.Profession)
                    .Include(project => project.Skills)
                    .Where(project => project.Users.Any(projectUser =>
                        projectUser.Id == user.Id))
                    .ToListAsync();
            }
            return user;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Find(userId) != null;
        }

        public bool UserExists(string username)
        {
            return _context.Users.AsNoTracking().Any(user => user.Username == username);
        }

        public async Task<User> AddAsync(User newUser, IEnumerable<int> skillIds)
        {
            newUser.Skills = await _context.Skills
                .Where(skill => skillIds.Any(id => id == skill.Id))
                .ToListAsync();

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> GetReadonlyByIdAsync(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(user => user.Id == userId)
                .FirstAsync();
            if (user != null)
                user = await HandleUserPrivacy(user);
            return user;
        }

        public async Task<User> GetReadonlyByUsernameAsync(string username)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(user => user.Username == username)
                .FirstOrDefaultAsync();
            if (user != null)
                user = await HandleUserPrivacy(user);
            return user;
        }

        public async Task<User> GetWriteableByIdAsync(int userId)
        {
            return await _context.Users
                .Include(user => user.Skills)
                .Where(user => user.Id == userId)
                .FirstAsync();
        }

        public async Task UpdateAsync(User updatedUser, IEnumerable<int> skillIds)
        {
            updatedUser.Skills = await _context.Skills
                .Where(skill => skillIds.Any(skillId => skillId == skill.Id))
                .ToListAsync();

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateViews(int userId, int[] projectIds)
        {
            var updatedUser = await _context.Users.FindAsync(userId);
            updatedUser.Viewed = updatedUser.Viewed.Union(projectIds).ToArray();

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClicks(int userId, int[] projectIds)
        {
            var updatedUser = await _context.Users.FindAsync(userId);
            updatedUser.Clicked = updatedUser.Clicked.Union(projectIds).ToArray();

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
