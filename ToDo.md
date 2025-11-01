# ToDo List for the League project

This is a ToDo list for the League project, which includes tasks related to the database schema, models, and other functionalities. Each task is categorized and includes a brief description of what needs to be done.

---

## Base Class for Models

Add a base class `BaseEntity` for all models in the League project. This class should include properties for `created_date` and `updated_date`. All models should inherit from this base class to ensure consistency across the application.
This will help in tracking the creation and modification dates of each entity in the database and will automatically update these fields when a record is created or modified.

### Base Class for Models - Tasks:

1. Create a base class for models named `BaseEntity` in the League project
   - This class should include properties for `CreatedDate` and `UpdatedDate`.
   - Ensure that all models in the project inherit from this base class.
   - Implement logic to automatically set the `CreatedDate` when a record is created and update
     the `UpdatedDate` when a record is modified.
2. Ensure all models inherit from the base class
3. Update the database schema to include `CreatedDate` and `UpdatedDate` fields in all models
4. Create a migration to apply changes to the database
5. Review and test the changes to ensure proper functionality.
6. Update the documentation to reflect the changes made to the models and database schema.
7. Ensure that the `CreatedDate` and `UpdatedDate` fields are properly indexed for performance
    optimization.
8. Create unit tests for the base class and ensure that the date fields are set correctly
    during creation and modification.

---

## Implement `IGenericRepository` and `GenericRepository`

Implement the `IGenericRepository` interface and the `GenericRepository` class to provide a generic way to interact with the database. This will allow for CRUD operations on any entity type without needing to create a specific repository for each entity.
This will help in reducing code duplication and improving maintainability.

## Soft Delete Functionality

==This is Overkill==

Implement soft delete functionality in the `GenericRepository` class. This will allow entities to be marked as deleted without actually removing them from the database. This is useful for maintaining data integrity and allowing for potential recovery of deleted records.
This will involve adding a `IsDeleted` property to the base entity class and updating the repository methods to respect this property when querying for entities.

### Soft Delete Functionality - Tasks:

1. Create an interface `ISoftDeletable` with properties for tracking deletion information (already completed)
    - Properties should include `IsDeleted`, `DeletedAt`, `DeletedReason`, and `DeletedByUserId`

2. Update existing entity models to implement `ISoftDeletable` interface
    - Modify all relevant entity classes to implement the interface
    - Add the required properties to each model class

```csharp
// Example entity implementation
public class Player : BaseEntity, ISoftDeletable
{
    // ... existing properties ...
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedReason { get; set; }
    public string? DeletedByUserId { get; set; }

}
```

3. Update database schema to include soft delete columns
    - Add `IsDeleted`, `DeletedAt`, `DeletedReason`, and `DeletedByUserId` fields to relevant tables
    - Create a migration to apply these changes to the database

4. Update existing repository implementations
    - Modify specific repositories like `PlayerRepository` to use soft delete functionality
    - Replace direct removal operations with soft delete calls

5. Update controllers and services to handle soft-deleted entities
    - Add options to include or exclude soft-deleted items in queries
    - Add functionality to restore soft-deleted items

6. Create unit tests for soft delete functionality
    - Test that entities are properly marked as deleted rather than removed
    - Test that queries filter out soft-deleted entities by default
    - Test restoration functionality

7. Add UI components to display and manage soft-deleted entities
    - Create views to see soft-deleted items
    - Add restore functionality to the user interface

8. Document the soft delete pattern for the project
    - Update API documentation to include information about soft delete
    - Document how to properly query including or excluding soft-deleted items


### This implementation:

- Adds soft delete functionality through ISoftDeletable interface
- Modifies DeleteAsync to perform soft delete when applicable
- Adds ApplySoftDeleteFilter helper method to filter out soft-deleted records
- Updates logging to include the includeDeleted parameter
- Properly handles the includeDeleted parameter in queries
- Ensures that soft-deleted entities are excluded from default queries
- Implements restoration functionality for soft-deleted entities

---

## Implement `IUnitOfWork` and `UnitOfWork`
