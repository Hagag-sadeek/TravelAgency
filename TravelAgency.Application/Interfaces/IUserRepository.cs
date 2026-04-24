using TravelAgency.Core.Entities;

namespace TravelAgency.Application.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<IEnumerable<Users>> GetAllWithRelationsAsync();
        Task<Users?> GetByIdWithRelationsAsync(int id);
        Task<Users?> GetByUsernameAsync(string username);
        Task<Users?> AuthenticateAsync(string username, string password);
    }
}
