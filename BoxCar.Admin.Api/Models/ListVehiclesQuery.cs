using System.ComponentModel.DataAnnotations;

namespace BoxCar.Admin.Api.Models
{
    public class ListVehiclesQuery
    {
        [Required]
        [MaxLength(100)]
        [MinLength(0)]
        public int PageSize = 50;

        [Required]
        [MinLength(0)]
        public int PageNumber { get; set; }

    }
}
