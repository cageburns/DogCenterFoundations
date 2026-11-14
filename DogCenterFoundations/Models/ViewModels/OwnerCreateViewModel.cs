using System.ComponentModel.DataAnnotations;

namespace DogCenterFoundations.Models.ViewModels;

public class OwnerCreateViewModel
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [Phone]
    [StringLength(30)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(1000)]
    public string? Notes { get; set; }
}