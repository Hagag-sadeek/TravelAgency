using TravelAgency.Application.DTOs.Users;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;

namespace TravelAgency.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ISupplierRepository _supplierRepository;

        public UserService(
            IUserRepository userRepository,
            IBranchRepository branchRepository,
            ISupplierRepository supplierRepository)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllWithRelationsAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdWithRelationsAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createDto)
        {
            var user = createDto.ToEntity();
            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var userWithRelations = await _userRepository.GetByIdWithRelationsAsync(createdUser.UserId);
            return userWithRelations!.ToDto();
        }

        public async Task<UserDto?> UpdateUserAsync(UpdateUserDto updateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(updateDto.UserId);
            if (existingUser == null)
            {
                return null;
            }

            updateDto.UpdateEntity(existingUser);
            await _userRepository.UpdateAsync(existingUser);
            await _userRepository.SaveChangesAsync();

            var userWithRelations = await _userRepository.GetByIdWithRelationsAsync(existingUser.UserId);
            return userWithRelations?.ToDto();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.AuthenticateAsync(username, password);
            return user?.ToDto();
        }

        public async Task<IEnumerable<(int Id, string Title)>> GetActiveBranchesAsync()
        {
            var branches = await _branchRepository.GetAllActiveAsync();
            return branches
                .Where(b => b.Title != null)
                .Select(b => (b.BranchId, b.Title!));
        }

        public async Task<IEnumerable<(int Id, string Name)>> GetActiveSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllActiveAsync();
            return suppliers
                .Where(s => s.FullName != null)
                .Select(s => (s.SupplierId, s.FullName!));
        }
    }
}
