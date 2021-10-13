using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class UserService : IService<User>
    {
        private readonly LagaltContext _context;

        // Constructor.
        public UserService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Messages)
                .Include(u => u.Skills)
                .Include(u => u.Projects)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int Id)
        {
            return await _context.Users
                .Include(u => u.Messages)
                .Include(u => u.Skills)
                .Include(u => u.Projects)
                .Where(u => u.Id == Id)
                .FirstAsync();
        }

        public async Task<User> AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public bool EntityExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
