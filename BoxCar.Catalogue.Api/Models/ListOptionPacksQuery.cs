using System.ComponentModel.DataAnnotations;

namespace BoxCar.Catalogue.Api.Models
{
    public class ListOptionPacksQuery
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
