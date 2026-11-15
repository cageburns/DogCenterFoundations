using System.ComponentModel.DataAnnotations;
namespace DogCenterFoundations.Models.ViewModels;

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
    [StringLength(30)]
    public string MicrochipId { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int OwnerId { get; set; }
       
    public bool IsVip { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }
}
