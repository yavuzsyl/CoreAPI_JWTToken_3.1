using CoreAPIWToken.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly TokenApiDBContext db;
        private readonly DbSet<T> entities;
        public BaseRepository(TokenApiDBContext db)
        {
            this.db = db;
            entities = db.Set<T>();
        }

        public async Task AddEntity(T entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task DeleteEntity(int id)
        {
            entities.Remove(await GetById(id));
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<int> GetCount(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
                return await entities.CountAsync(expression);
            return await entities.CountAsync();
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
                return await entities.Where(expression).ToListAsync();
            return await entities.ToListAsync();
        }

        public void UpdateEntity(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
