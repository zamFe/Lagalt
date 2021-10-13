using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class MessageService : IService<Message>
    {

        private readonly LagaltContext _context;

        public MessageService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages
                .Include(m => m.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetByProjectIdAsync(int id) {
            return await _context.Messages
                .Include(m => m.User)
                .Where(m => m.ProjectId == id)
                .ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages
                .Include(m => m.User)
                .Where(m => m.Id == id)
                .FirstAsync();
        }

        public async Task<Message> AddAsync(Message entity)
        {
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool EntityExists(int id)
        {
            return _context.Messages.Any(m => m.Id == id);
        }

    }
}
