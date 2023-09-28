using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int userId)
        {
            return await _dbSet.Where(e => EF.Property<int>(e, "UserId") == userId).ToListAsync();
        }

        public async Task<T> GetById(int id, int userId)
        {
            var entity = await _dbSet
                .Where(e => EF.Property<int>(e, "UserId") == userId)
                .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            return entity;
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete is not null)
            {
                _dbSet.Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Object Not Found");
            }
        }
    }
}
