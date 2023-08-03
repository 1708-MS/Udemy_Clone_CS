using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Application.Interfaces.IRepository
{
    /// <summary>
    /// The IRepository Interface provides a generic contract for performing CRUD (Create, Read, Update, Delete) operations on entities.
    /// </summary>
    /// <typeparam name="T">T is the type of entity for which the Repository is created. Entity means Domain Models</typeparam>
    public interface IRepository<T> where T : class
    {
        // Adds an entity to the database asynchronously.
        Task AddAsync(T entity);

        // Removes an entity from the database.
        void Remove(T entity);

        //Removes an entity from the database asynchronously based on specified Id
        Task RemoveAsync(int id);

        // Removes a collection of entities from the database.
        void RemoveRange(IEnumerable<T> entity);

        // Find/Retrieves an entity from the database based on the specified Id asynchronously
        Task<T> GetAsync(int id);

        // Display/Retrieves all entities from the database asynchronously
        Task<IEnumerable<T>> GetAllAsync(
            // Filters the Entities
            Expression<Func<T, bool>> Filter = null,
            // Sorting to Order the Entities
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            // Multiple list (as a Comma-separated list) of the navigation properties that can ve included with the Entities 
            string includeproperties = null 
            );
       
        // Fetches/Retreives the First Entity that matches with the specified filter from the database
        T FirstOrDefault(
            // Optional filter expression to apply to the entity.
            Expression<Func<T, bool>> Filter = null,
            // Multiple list (as a Comma-separated list) of the navigation properties to be included with the Entities 
            string includeproperties = null
            );
    }
}
