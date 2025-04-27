using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace League.Server.Data.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface providing common data access operations for any entity
    /// </summary>
    /// <typeparam name="T">The entity type for which the repository is created</typeparam>
    public interface IGenericRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets all entities of type T
        /// </summary>
        /// <returns>A collection of all entities</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity if found; otherwise, null</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Finds entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <returns>A collection of matching entities</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity by ID, using soft delete if the entity implements ISoftDeletable
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <returns>A collection of matching entities</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets entities that match the specified predicate with ordering
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <returns>A collection of matching entities</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy
        );

        /// <summary>
        /// Gets entities that match the specified predicate with pagination and ordering
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <returns>A collection of matching entities</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take
        );

        /// <summary>
        /// Gets entities that match the specified predicate with pagination, ordering, and includes
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <param name="includes">Related entities to include</param>
        /// <returns>A collection of matching entities with their related data</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            params Expression<Func<T, object>>[] includes
        );

        /// <summary>
        /// Gets entities that match the specified predicate with pagination, ordering, includes, and tracking options
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <param name="disableTracking">Whether to disable change tracking</param>
        /// <param name="includes">Related entities to include</param>
        /// <returns>A collection of matching entities with their related data</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            bool disableTracking,
            params Expression<Func<T, object>>[] includes
        );

        /// <summary>
        /// Gets entities that match the specified predicate with pagination, ordering, includes, and tracking options
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <param name="disableTracking">Whether to disable change tracking</param>
        /// <param name="asNoTracking">Whether to use AsNoTracking</param>
        /// <param name="includes">Related entities to include</param>
        /// <returns>A collection of matching entities with their related data</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            bool disableTracking,
            bool asNoTracking,
            params Expression<Func<T, object>>[] includes
        );

        /// <summary>
        /// Gets entities that match the specified predicate with full filtering options
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <param name="disableTracking">Whether to disable change tracking</param>
        /// <param name="asNoTracking">Whether to use AsNoTracking</param>
        /// <param name="includeDeleted">Whether to include soft-deleted records</param>
        /// <param name="includes">Related entities to include</param>
        /// <returns>A collection of matching entities with their related data</returns>
        Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            bool disableTracking,
            bool asNoTracking,
            bool includeDeleted,
            params Expression<Func<T, object>>[] includes
        );
    }
}
