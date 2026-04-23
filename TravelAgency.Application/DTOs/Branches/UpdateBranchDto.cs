using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Branches
{
    public class UpdateBranchDto
    {
        public int BranchId { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int BranchOrder { get; set; }

        public string? Address { get; set; }

        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
