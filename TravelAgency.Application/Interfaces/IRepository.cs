namespace TravelAgency.Application.Interfaces
{
    /// <summary>
    /// Generic repository interface for common CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get entity by ID
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Add new entity
        /// </summary>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update existing entity
        /// </summary>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete entity by ID (soft delete when applicable)
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Check if entity exists
        /// </summary>
        Task<bool> ExistsAsync(int id);

        /// <summary>
        /// Save changes to database
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
