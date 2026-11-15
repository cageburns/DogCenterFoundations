using System.ComponentModel.DataAnnotations;

namespace DogCenterFoundations.Models.ViewModels
{
    public class ServiceCreateViewModel
    {
        [Required]
        public string ServiceName { get; set; } = null!;

        [Required]
        public string ServiceType { get; set; } = null!;

        [Range(0.01, 999.99)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = null!;
    }
}