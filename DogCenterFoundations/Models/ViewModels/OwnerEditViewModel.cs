using System.ComponentModel.DataAnnotations;

namespace DogCenterFoundations.Models.ViewModels;


public class OwnerEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\+?[0-9]+$", ErrorMessage = "Phone number may only contain digits and +.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(1000)]
    public string? Notes { get; set; }
}