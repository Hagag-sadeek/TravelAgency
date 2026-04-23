namespace TravelAgency.Application.DTOs.Customers
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string? Code { get; set; }
        public string? Job { get; set; }
        public string? FullName { get; set; }
        public string? Adreess1 { get; set; }
        public string? Adreess2 { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public bool IsActive { get; set; } = true;
        public bool SendWhatsApp { get; set; }
        public bool NeedUpdate { get; set; }
        public bool Updated { get; set; }
        public int Points { get; set; }
    }
}
