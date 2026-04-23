# Repository Pattern - Hybrid Approach

## Overview
This project uses a **Hybrid Repository Pattern** combining:
- Generic base repository for common CRUD operations
- Specific repositories for entity-specific complex queries

## Architecture

```
IRepository<T>                    (Generic interface - common CRUD)
    ↑
    |
ISupplierRepository : IRepository<Suppliers>  (Specific interface - custom queries)
    ↑
    |
Repository<T>                     (Generic implementation)
    ↑
    |
SupplierRepository : Repository<Suppliers>    (Specific implementation)
```

## When to Use What

### ✅ Use Generic Repository Only
For simple entities with only basic CRUD:
```csharp
// If you only need: GetById, GetAll, Add, Update, Delete
services.AddScoped<IRepository<SimpleEntity>, Repository<SimpleEntity>>();
```

### ✅ Use Specific Repository
For entities with complex queries or business logic:
```csharp
// When you need custom queries like:
// - GetAllActiveAsync()
// - GetWithRelatedDataAsync()
// - SearchAsync()
// - Complex filtering

public interface ICustomerRepository : IRepository<Customers>
{
    Task<IEnumerable<Customers>> GetAllActiveAsync();
    Task<Customers?> GetByPhoneAsync(string phone);
}
```

## Examples

### Simple Entity (Use Generic Only)
```csharp
// Registration
services.AddScoped<IRepository<TicketDistributions>, Repository<TicketDistributions>>();

// Usage in Service
public class TicketDistributionService
{
    private readonly IRepository<TicketDistributions> _repository;

    public async Task<IEnumerable<TicketDistributionDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => e.ToDto());
    }
}
```

### Complex Entity (Use Specific Repository)
```csharp
// Interface
public interface ITicketRepository : IRepository<Tickets>
{
    Task<IEnumerable<Tickets>> GetByDateAndAppointmentAsync(DateTime date, int appointmentId);
    Task<IEnumerable<Tickets>> GetWithCustomerAndSupplierAsync();
}

// Implementation
public class TicketRepository : Repository<Tickets>, ITicketRepository
{
    public TicketRepository(TravelAgencyContext context) : base(context) { }

    public async Task<IEnumerable<Tickets>> GetByDateAndAppointmentAsync(
        DateTime date, int appointmentId)
    {
        return await _dbSet
            .Include(t => t.Customer)
            .Include(t => t.Supplier)
            .Include(t => t.Appointment)
            .Where(t => t.TicketDate.Date == date.Date && 
                        t.AppointmentId == appointmentId)
            .ToListAsync();
    }
}
```

## Benefits

1. **DRY Principle**: Common CRUD in one place
2. **Flexibility**: Custom queries where needed
3. **Testability**: Easy to mock
4. **Consistency**: Standard patterns across project
5. **Smart Soft Delete**: Automatically uses IsActive property if available

## Available Methods (Generic Base)

All repositories inheriting from `Repository<T>` get:
- `GetByIdAsync(int id)`
- `GetAllAsync()`
- `AddAsync(TEntity entity)`
- `UpdateAsync(TEntity entity)`
- `DeleteAsync(int id)` - Smart soft/hard delete
- `ExistsAsync(int id)`
- `SaveChangesAsync()`

## Migration Checklist

- [x] Suppliers (Specific - has custom queries)
- [x] Customers (Specific - has search functionality)
- [x] Branches (Specific - has ordering)
- [ ] Tickets (Specific - complex joins needed)
- [ ] Appointments (Specific - custom filtering)
- [ ] Users (Specific - authentication logic)
- [ ] AppointmentDetails (Generic - simple CRUD)
- [ ] TicketDistributions (Generic - simple CRUD)
- [ ] AppointmentPrice (Generic - simple CRUD)
- [ ] CustomerCancels (Generic - simple CRUD)

## Notes
- Generic repository automatically handles soft delete for entities with `IsActive` property
- Protected `_dbSet` allows derived classes to write custom queries
- All repositories use async/await for better performance
