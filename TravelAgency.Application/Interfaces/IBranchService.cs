using TravelAgency.Application.DTOs.Branches;

namespace TravelAgency.Application.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDto>> GetAllActiveBranchesAsync();
        Task<IEnumerable<BranchDto>> GetAllOrderedBranchesAsync();
        Task<BranchDto?> GetBranchByIdAsync(int id);
        Task<BranchDto> CreateBranchAsync(CreateBranchDto createDto);
        Task<BranchDto?> UpdateBranchAsync(UpdateBranchDto updateDto);
        Task<bool> DeleteBranchAsync(int id);
    }
}
