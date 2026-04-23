using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Customers
{
    public class QuickAddCustomerDto
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقم")]
        [RegularExpression(@"^01[0-2,5]\d{8}$", ErrorMessage = "رقم الهاتف غير صحيح")]
        public string Phone { get; set; } = string.Empty;
    }
}
