using TravelAgency.Application.DTOs.Users;

namespace TravelAgency.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto createDto);
        Task<UserDto?> UpdateUserAsync(UpdateUserDto updateDto);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto?> AuthenticateAsync(string username, string password);
        Task<IEnumerable<(int Id, string Title)>> GetActiveBranchesAsync();
        Task<IEnumerable<(int Id, string Name)>> GetActiveSuppliersAsync();
    }
}
