using Microsoft.EntityFrameworkCore;
using Proyecto_trivia_BED.ContextoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly TriviaContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(TriviaContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
            
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includes = "")
        {
            IQueryable<T> query = _dbSet;

            //Filtrar por condiciones where
            if (where != null)
            {
                query = query.Where(where);
            }

            //Agregar propiedades include
            if (!string.IsNullOrWhiteSpace(includes))
            {
                foreach (var includeProperty in includes.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            //Aplicar orderBy
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
