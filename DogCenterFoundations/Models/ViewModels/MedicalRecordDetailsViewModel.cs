namespace DogCenterFoundations.Models.ViewModels;

public class MedicalRecordDetailsViewModel
{
    public int Id { get; set; }

    public int DogId { get; set; }

    public string DogName { get; set; } = string.Empty;

    public string RecordType { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string? Brand { get; set; }

    public DateTime? NextDueDate { get; set; }

    public string? Notes { get; set; }
}
