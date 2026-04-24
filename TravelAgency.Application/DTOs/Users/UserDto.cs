namespace TravelAgency.Application.DTOs.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public int BranchId { get; set; }
        public string? BranchTitle { get; set; }
        public bool IsAdmin { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
    }
}
