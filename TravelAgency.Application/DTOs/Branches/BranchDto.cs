namespace TravelAgency.Application.DTOs.Branches
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public int BranchOrder { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
