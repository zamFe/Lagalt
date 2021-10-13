using LagaltAPI.Context;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public class ApplicationService : IService<Application>
    {
        private readonly LagaltContext _context;

        // Constructor.
        public ApplicationService(LagaltContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetByProjectIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.User)
                .Where(a => a.ProjectId == id)
                .ToListAsync();
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.User)
                .Where(a => a.Id == id)
                .FirstAsync();
        }

        public async Task<Application> AddAsync(Application entity)
        {
            _context.Applications.Add(entity);
            await _context.SaveChangesAsync();
            entity.User = await _context.Users.FindAsync(entity.UserId);
            return entity;
        }

        // TODO - Split larger interface into smaller ones.
        //        Following methods are not exposed through the API.
        public Task<IEnumerable<Application>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Application entity)
        {
            throw new NotImplementedException();
        }

        public bool EntityExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
