﻿using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class MessageService
    {
        private readonly LagaltContext _context;

        // Constructor.
        public MessageService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<Message> AddAsync(Message newMessage)
        {
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();
            newMessage.User = await _context.Users.FindAsync(newMessage.UserId);
            return newMessage;
        }

        public async Task<Message> GetByIdAsync(int messageId)
        {
            return await _context.Messages
                .Include(message => message.User)
                .Where(message => message.Id == messageId)
                .FirstAsync();
        }

        public async Task<IEnumerable<Message>> GetByProjectIdAsync(int projectId)
        {
            return await _context.Messages
                .Include(message => message.User)
                .Where(message => message.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
