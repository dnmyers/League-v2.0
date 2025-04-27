using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using League.Server.Data;
using League.Server.Data.Repositories.Interfaces;
using League.Server.Models;
using League.Server.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace League.Server.Data.Repositories
{
    /// <summary>
    /// Generic repository implementation that provides common data access operations for any entity
    /// </summary>
    /// <typeparam name="T">The entity type for which the repository is created</typeparam>
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly LeagueDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null</exception>
        public GenericRepository(LeagueDbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all entities of type T
        /// </summary>
        /// <returns>A collection of all entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all records of type {EntityType}", typeof(T).Name);
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity if found; otherwise, null</returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            _logger.LogInformation(
                "Fetching record with ID {Id} of type {EntityType}",
                id,
                typeof(T).Name
            );
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Finds entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <returns>A collection of matching entities</returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with condition: {Condition}",
                typeof(T).Name,
                predicate
            );
            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                _logger.LogWarning("Attempted to add a null entity.");
                return;
            }

            _logger.LogInformation(
                "Adding a new record of type {EntityType}: {Entity}",
                typeof(T).Name,
                entity
            );
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                _logger.LogWarning("Attempted to update a null entity.");
                return;
            }

            _logger.LogInformation(
                "Updating a record of type {EntityType}: {Entity}",
                typeof(T).Name,
                entity
            );
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                _logger.LogWarning("Attempted to delete a null entity.");
                return;
            }

            _logger.LogInformation(
                "Deleting record of type {EntityType}: {Entity}",
                typeof(T).Name,
                entity
            );

            if (entity is ISoftDeletable softDeletableEntity)
            {
                _logger.LogDebug(
                    "Soft deleting record of type {EntityType}: {Entity}",
                    typeof(T).Name,
                    entity
                );

                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
            }
            else
            {
                _logger.LogDebug(
                    "Hard deleting record of type {EntityType}: {Entity}",
                    typeof(T).Name,
                    entity
                );
                _dbSet.Remove(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes an entity by ID, using soft delete if the entity implements ISoftDeletable
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        /// <param name="cancellationToken">Optional token to cancel the operation</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Deleting record with ID {Id} of type {EntityType}",
                id,
                typeof(T).Name
            );

            var entity = await GetByIdAsync(id);
            if (entity is null)
            {
                _logger.LogWarning("Record with ID {Id} not found for deletion.", id);
                return;
            }

            await DeleteAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Gets entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <returns>A collection of matching entities</returns>
        public async Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate}",
                typeof(T).Name,
                predicate
            );
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Gets entities that match the specified predicate with ordering
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <returns>A collection of matching entities</returns>
        public async Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy
        )
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate} and ordering",
                typeof(T).Name,
                predicate
            );
            return await orderBy(_dbSet.Where(predicate)).ToListAsync();
        }

        /// <summary>
        /// Gets entities that match the specified predicate with pagination and ordering
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <returns>A collection of matching entities</returns>
        public async Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take
        )
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate}, ordering, skipping {Skip}, taking {Take}",
                typeof(T).Name,
                predicate,
                skip,
                take
            );
            return await orderBy(_dbSet.Where(predicate)).Skip(skip).Take(take).ToListAsync();
        }

        /// <summary>
        /// Gets entities that match the specified predicate with pagination, ordering, and includes
        /// </summary>
        /// <param name="predicate">The condition to apply</param>
        /// <param name="orderBy">The ordering function</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to take</param>
        /// <param name="includes">Related entities to include</param>
        /// <returns>A collection of matching entities with their related data</returns>
        public async Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            params Expression<Func<T, object>>[] includes
        )
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate}, ordering, skipping {Skip}, taking {Take}",
                typeof(T).Name,
                predicate,
                skip,
                take
            );

            IQueryable<T> query = _dbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await orderBy(query).Skip(skip).Take(take).ToListAsync();
        }

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
        public async Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            bool disableTracking,
            params Expression<Func<T, object>>[] includes
        )
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate}, ordering, skipping {Skip}, taking {Take}",
                typeof(T).Name,
                predicate,
                skip,
                take
            );

            IQueryable<T> query = disableTracking ? _dbSet.AsNoTracking() : _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await orderBy(query).Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

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
        public async Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            bool disableTracking,
            bool asNoTracking,
            params Expression<Func<T, object>>[] includes
        )
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate}, ordering, skipping {Skip}, taking {Take}",
                typeof(T).Name,
                predicate,
                skip,
                take
            );

            IQueryable<T> query = _dbSet;

            if (disableTracking || asNoTracking)
            {
                query = query.AsNoTracking();
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await orderBy(query).Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

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
        public async Task<IEnumerable<T>> GetByPredicateAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            bool disableTracking,
            bool asNoTracking,
            bool includeDeleted,
            params Expression<Func<T, object>>[] includes
        )
        {
            _logger.LogInformation(
                "Fetching records of type {EntityType} with predicate: {Predicate}, ordering, skipping {Skip}, taking {Take}, includeDeleted: {IncludeDeleted}",
                typeof(T).Name,
                predicate,
                skip,
                take,
                includeDeleted
            );

            if (predicate is null)
            {
                _logger.LogWarning("Predicate is null, returning empty list.");
                return Array.Empty<T>();
            }

            IQueryable<T> query = _dbSet;

            if (disableTracking || asNoTracking)
            {
                query = query.AsNoTracking();
            }

            query = ApplySoftDeleteFilter(query, includeDeleted);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await orderBy(query).Where(predicate).Skip(skip).Take(take).ToListAsync();
        }

        /// <summary>
        /// Applies soft delete filtering to a query if applicable
        /// </summary>
        /// <param name="query">The query to filter</param>
        /// <param name="includeDeleted">Whether to include soft-deleted records</param>
        /// <returns>The filtered query</returns>
        private IQueryable<T> ApplySoftDeleteFilter(IQueryable<T> query, bool includeDeleted)
        {
            if (!includeDeleted && typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(x => !((ISoftDeletable)x).IsDeleted);
            }

            return query;
        }
    }
}
