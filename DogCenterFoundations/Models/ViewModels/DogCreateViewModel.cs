using System.ComponentModel.DataAnnotations;
namespace DogCenterFoundations.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class DogCreateViewModel
{
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(30)]
    public string Breed { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    [Required]
    [RegularExpression(@"^[0-9]{15}$",
    ErrorMessage = "Microchip ID must contain exactly 15 digits.")]
    public string MicrochipId { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int OwnerId { get; set; }

    [ValidateNever]
    public IEnumerable<OwnerListViewModel> Owners { get; set; } = new List<OwnerListViewModel>();

    public bool IsVip { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }
}