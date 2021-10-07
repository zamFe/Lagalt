using LagaltAPI.Context;
using LagaltAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Repositories
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
                .ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int Id)
        {
            return await _context.Messages
                .Where(m => m.Id == Id)
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
