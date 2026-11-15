using System.ComponentModel.DataAnnotations;

namespace DogCenterFoundations.Models.ViewModels;

public class MedicalRecordEditViewModel
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Inalid dog")]
    public int DogId { get; set; }

    [Required]
    public string RecordType { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Brand { get; set; }

    [DataType(DataType.Date)]
    public DateTime? NextDueDate { get; set; }

    public string? Notes { get; set; }
}
