namespace DogCenterFoundations.Models.Entities;

public class Dog
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string MicrochipId { get; set; } = null!;

    public int OwnerId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public bool IsVip { get; set; }

    public string? Notes { get; set; }

    public Owner Owner { get; set; } = null!;

    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}