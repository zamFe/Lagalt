using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using LagaltAPI.Models.Wrappers;
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
            var user = await _context.Users
                .Include(user => user.Skills)
                .Where(user => user.Id == newMessage.UserId)
                .FirstAsync();
            user.ContributedTo = user.AppliedTo.Union(new int[] {newMessage.ProjectId}).ToArray();
            newMessage.User = user;

            _context.Entry(user).State = EntityState.Modified;
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();
            return newMessage;
        }

        public async Task<Message> GetByIdAsync(int messageId)
        {
            return await _context.Messages
                .AsNoTracking()
                .Include(message => message.User)
                .Where(message => message.Id == messageId)
                .FirstAsync();
        }

        public async Task<IEnumerable<Message>> GetPageByProjectIdAsync(
            int projectId, PageRange range)
        {
            return await _context.Messages
                .AsNoTracking()
                .Include(message => message.User)
                .Where(message => message.ProjectId == projectId)
                .OrderByDescending(message => message.Id)
                .Skip(range.Offset - 1)
                .Take(range.Limit)
                .ToListAsync();
        }

        public async Task<int> GetTotalProjectMessagesAsync(int projectId)
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(message => message.ProjectId == projectId)
                .CountAsync();
        }
    }
}
