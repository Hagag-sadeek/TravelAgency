using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
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
