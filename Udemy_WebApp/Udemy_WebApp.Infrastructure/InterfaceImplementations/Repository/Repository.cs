using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Infrastructure.DataAccess;

namespace Udemy_WebApp.Infrastructure.InterfaceImplementations.Repository
{
    /// <summary>
    /// Generic implementation of the IRepository interface for performing CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity for which the repository is implemented</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }

        /// <summary>
        /// Adds the specified entity to the database asynchronously
        /// </summary>
        /// <param name="entity">The entity to be added</param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
        }

        /// <summary>
        /// Fetches/Retrieves the first entity that matches the specified filter from the database.
        /// </summary>
        /// <param name="Filter">Optional filter expression that can be applied to the entity</param>
        /// <param name="includeproperties">Multiple list (as a Comma-separated list) of the navigation properties that can ve included with the Entities </param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> Filter = null, string includeproperties = null)
        {
            IQueryable<T> query = dbset;
            if (Filter != null)
                query = query.Where(Filter);
            if (includeproperties != null)
            {
                foreach (var includeprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(int id)
        {
            //return dbset.Find(id);
            return await dbset.FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filter"></param>
        /// <param name="orderby"></param>
        /// <param name="includeproperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> Filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeproperties = null)
        {
            //display
            IQueryable<T> query = dbset;
            if (Filter != null)
                query = query.Where(Filter);
            if (includeproperties != null)
            {
                foreach (var includeprop in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            //sorting
            if (orderby != null)
                return orderby(query).ToList();
            return query.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(int id)
        {
            dbset.Remove(await dbset.FindAsync(id));
            //var entity = dbset.Find(id);
            //dbset.Remove(entity); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }
    }
}