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

        public bool EntityExists(int userId)
        {
            return _context.Users.Any(user => user.Id == userId);
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

        // TODO - Remove unused functionality.
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(user => user.Messages)
                .Include(user => user.Skills)
                .Include(user => user.Projects)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users
                .Include(user => user.Messages)
                .Include(user => user.Skills)
                .Include(user => user.Projects)
                .Where(user => user.Id == userId)
                .FirstAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(user => user.Messages)
                .Include(user => user.Skills)
                .Include(user => user.Projects)
                .Where(user => user.Username == username)
                .FirstAsync();
        }

        public async Task UpdateAsync(User updatedUser, IEnumerable<int> skillIds)
        {
            updatedUser.Skills = await _context.Skills
                .Where(skill => skillIds.Any(id => id == skill.Id))
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
