using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.CustomerCancels
{
    public class CreateCustomerCancelDto
    {
        [Required(ErrorMessage = "رقم العميل مطلوب")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "التعليق مطلوب")]
        public string Comment { get; set; } = string.Empty;
    }
}
