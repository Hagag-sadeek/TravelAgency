using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "الفرع مطلوب")]
        public int BranchId { get; set; }

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "المورد مطلوب")]
        public int SupplierId { get; set; }
    }
}
