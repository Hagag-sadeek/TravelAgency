using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Core.Entities
{
    public partial class CustomerCancels
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }


        public string? comment { get; set; }

        public DateTime CancelDate { get; set; }


    }
}
