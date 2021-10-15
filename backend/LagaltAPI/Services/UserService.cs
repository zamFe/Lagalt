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

        public async Task<User> AddAsync(User newUser, List<int> SkillIds)
        {
            List<Skill> skills = await _context.Skills
                .Where(s => SkillIds.Any(id => id == s.Id))
                .ToListAsync();

            newUser.Skills = skills;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

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

        public async Task UpdateAsync(User updatedUser, List<int> SkillIds)
        {
            List<Skill> skills = await _context.Skills
                .Where(s => SkillIds.Any(id => id == s.Id))
                .ToListAsync();

            updatedUser.Skills = skills;

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
