﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagaltAPI.Services
{
    public interface IService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int Id);
        public Task<T> AddAsync(T entity);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(T entity);
        public bool EntityExists(int id);

    }
}
