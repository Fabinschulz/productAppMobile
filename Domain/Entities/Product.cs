using ProductAppMAUI.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace ProductAppMAUI.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
