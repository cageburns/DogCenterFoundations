namespace DogCenter.Models.Entities;

public class MedicalRecord
{
    public int Id { get; set; }

    public int DogId { get; set; }

    public string RecordType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Brand { get; set; }

    public DateTime? NextDueDate { get; set; }

    public string? Notes { get; set; }

    public Dog Dog { get; set; } = null!;
}