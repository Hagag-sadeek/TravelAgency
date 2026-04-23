using TravelAgency.Application.DTOs.Branches;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.Mappings;

namespace TravelAgency.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;

        public BranchService(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BranchDto>> GetAllActiveBranchesAsync()
        {
            var branches = await _repository.GetAllActiveAsync();
            return branches.Select(b => b.ToDto());
        }

        public async Task<IEnumerable<BranchDto>> GetAllOrderedBranchesAsync()
        {
            var branches = await _repository.GetAllAsync();
            return branches.OrderBy(b => b.BranchOrder).Select(b => b.ToDto());
        }

        public async Task<BranchDto?> GetBranchByIdAsync(int id)
        {
            var branch = await _repository.GetByIdAsync(id);
            return branch?.ToDto();
        }

        public async Task<BranchDto> CreateBranchAsync(CreateBranchDto createDto)
        {
            var branch = createDto.ToEntity();
            var createdBranch = await _repository.AddAsync(branch);
            await _repository.SaveChangesAsync();
            return createdBranch.ToDto();
        }

        public async Task<BranchDto?> UpdateBranchAsync(UpdateBranchDto updateDto)
        {
            var existingBranch = await _repository.GetByIdAsync(updateDto.BranchId);
            if (existingBranch == null)
            {
                return null;
            }

            updateDto.UpdateEntity(existingBranch);
            await _repository.UpdateAsync(existingBranch);
            await _repository.SaveChangesAsync();

            return existingBranch.ToDto();
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await _repository.SaveChangesAsync();
            }
            return result;
        }
    }
}
