using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Application.DTOs.Suppliers
{
    public class UpdateSupplierDto
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string? FullName { get; set; }

        [Range(0, 100, ErrorMessage = "العمولة يجب أن تكون بين 0 و 100")]
        public int Commision { get; set; }

        public int SupplierOrder { get; set; }

        public string? Adreess1 { get; set; }

        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? Phone1 { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
