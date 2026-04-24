using Microsoft.EntityFrameworkCore;
using TravelAgency.Application.Interfaces;
using TravelAgency.Core.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(TravelAgencyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Users>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(u => u.Branch)
                .Include(u => u.Supplier)
                .ToListAsync();
        }

        public async Task<Users?> GetByIdWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Branch)
                .Include(u => u.Supplier)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<Users?> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<Users?> AuthenticateAsync(string username, string password)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
