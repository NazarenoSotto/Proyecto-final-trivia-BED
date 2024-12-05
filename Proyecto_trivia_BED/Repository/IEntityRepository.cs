using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Proyecto_trivia_BED.Repository
{
    interface IEntityRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> where = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includes = "");

        Task CreateAsync(T entity);

        Task SaveChangesAsync();
    }
}
