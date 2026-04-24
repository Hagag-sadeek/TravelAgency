using System;

namespace TravelAgency.Application.DTOs.CustomerCancels
{
    public class CustomerCancelDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Comment { get; set; }
        public DateTime CancelDate { get; set; }
    }
}
