using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
