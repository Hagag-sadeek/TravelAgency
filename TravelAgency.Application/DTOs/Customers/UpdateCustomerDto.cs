using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Customers
{
    public class UpdateCustomerDto
    {
        public int CustomerId { get; set; }

        public string? Code { get; set; }

        public string? Job { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string? FullName { get; set; }

        public string? Adreess1 { get; set; }

        public string? Adreess2 { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقم")]
        public string? Phone1 { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقم")]
        public string? Phone2 { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقم")]
        public string? Phone3 { get; set; }

        public bool IsActive { get; set; } = true;
        public bool SendWhatsApp { get; set; }
        public bool NeedUpdate { get; set; }
        public bool Updated { get; set; }
        public int Points { get; set; }
    }
}
