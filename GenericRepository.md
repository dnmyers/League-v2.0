# `GenericRepository`

## Overview
The `GenericRepository<T>` class is a generic implementation of the repository pattern, which provides a way to manage data access and manipulation for different entities in a consistent manner. It abstracts the underlying data access technology (Entity Framework Core) and provides a set of common methods for CRUD operations.

## Features
- Type-safe data access through generics
- Support for both hard and soft deletion
- Asynchronous operations with cancellation token support
- Comprehensive logging
- Flexible querying with predicates
- Eager loading of related entities
- Pagination support
- Optional tracking control for better performance

## Key Methods

### Basic CRUD Operations
- `GetAllAsync()` - Retrieves all records of the entity type
- `GetByIdAsync(int id)` - Retrieves a specific entity by its ID
- `AddAsync(T entity)` - Adds a new entity to the database
- `UpdateAsync(T entity)` - Updates an existing entity
- `DeleteAsync(int id)` - Deletes an entity (supports soft deletion if entity implements ISoftDeletable)

### Advanced Querying
- `FindAsync(Expression<Func<T, bool>> predicate)` - Finds entities matching a condition
- `GetByPredicateAsync(Expression<Func<T, bool>> predicate)` - Retrieves entities based on a predicate
- `GetByPredicateAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int skip, int take, ...)` - Advanced querying with ordering and pagination

#### Key Points About `GetByPredicateAsync`:
- **Predicate**: A function that defines the condition to filter entities.
- **OrderBy**: An optional function to specify the ordering of the results.
- **Skip**: Number of records to skip (for pagination).
- **Take**: Number of records to take (for pagination).
- **CancellationToken**: Allows for cancellation of the operation, useful in long-running queries.
- **Tracking**: Optionally control whether the returned entities are tracked by the context.
- **Include**: Specify related entities to include in the query (eager loading).
- **AsNoTracking**: Optionally disable change tracking for better performance when the entities are not modified.


## Usage Example

- How to use `GetByPredicateAsync` to find players by team ID:

```csharp
// Example usage in a service or controller
public class PlayerService
{
    private readonly IGenericRepository<Player> _playerRepository;

    public PlayerService(IGenericRepository<Player> playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(int teamId, CancellationToken cancellationToken)
    {
        // Create the predicate to filter players by team ID
        Expression<Func<Player, bool>> teamPredicate = player => player.TeamId == teamId;

        // Use the repository method to find players
        return await _playerRepository.GetByPredicateAsync(
            teamPredicate,
            orderBy: players => players.OrderBy(p => p.Name), // Optional ordering
            skip: 0, // No skipping for this example
            take: 10, // Limit to 10 players
            cancellationToken: cancellationToken // Pass the cancellation token
        );
    }
}
```

- Could also use `return await _playerRepository.GetByPredicateAsync(teamPredicate);` to get all players without pagination.

1. The method takes a LINQ expression that defines your filtering condition.
2. You can create more complex predicates:

```csharp
// Find active players in a specific team
Expression<Func<Player, bool>> activePlayers = player => player.TeamId == teamId && player.IsActive = true;

// Find players by position in a team
Expression<Func<Player, bool>> goalkeepers = player => player.TeamId == teamId && player.Position == "Goalkeeper";
```

3. If you need to include related data or order results, use one of the overloads:

```csharp
// Including related data and ordering
public async Task<IEnumerable<Player>> GetPlayersByTeamIdWithDetailsAsync(int teamId)
{
    Expression<Func<Player, bool>> teamPredicate = player => player.TeamId == teaamId;

    // Order by player name
    Func<IQueryable<Player>, IOrderedQueryable<Player>> orderByName = query => query.OrderBy(p => p.Name);

    // Include related Stats and Contract
    return await _playerRepository.GetByPredicateAsync(
        predicate: teamPredicate,
        orderBy: orderByName,
        skip: 0,
        take: 100,
        includes: new Expression<Func<Player, object>>[]
        {
            player => player.Stats,
            player => player.Contract
        }
    );
}
```
- This method is very flexible and can be used for any type of filtering condition that can be expressed as a LINQ expression. The predicate is type-safe and will be translated to SQL by Entity Framework Core.

- This example retrieves players from a specific team, orders them by name, and includes their stats and contract information.
- The `includes` parameter is an array of expressions that specify the related entities to include in the query. This allows for eager loading of related data, which can help reduce the number of database queries and improve performance when accessing related entities.
- The `skip` and `take` parameters are used for pagination, allowing you to control how many records to skip and how many to take in the result set. This is useful for implementing paging in your application.
- The `orderBy` parameter is a function that specifies how to order the results. In this case, it orders the players by their name in ascending order.
- The `predicate` parameter is a LINQ expression that defines the condition to filter the players. In this case, it filters players by their team ID.
- The `cancellationToken` parameter allows you to cancel the operation if needed, which is useful for long-running queries or when the user navigates away from the page.
- The `tracking` parameter is a boolean that specifies whether the returned entities should be tracked by the context. If set to `false`, the entities will not be tracked, which can improve performance when you don't need to modify them.
- The `asNoTracking` parameter is a boolean that specifies whether to use the `AsNoTracking` method for the query. If set to `true`, the entities will not be tracked by the context, which can improve performance when you don't need to modify them.
- The `includes` parameter is an array of expressions that specify the related entities to include in the query. This allows for eager loading of related data, which can help reduce the number of database queries and improve performance when accessing related entities.
