namespace TravelAgency.Application.DTOs.Suppliers
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public int Commision { get; set; }
        public int SupplierOrder { get; set; }
        public string? FullName { get; set; }
        public string? Adreess1 { get; set; }
        public string? Phone1 { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
