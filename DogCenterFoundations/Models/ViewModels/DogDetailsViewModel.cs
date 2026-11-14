namespace DogCenterFoundations.Models.ViewModels;

public class DogDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string MicrochipId { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public bool IsVip { get; set; }

    public string? Notes { get; set; }

    public int OwnerId { get; set; }

    public string OwnerName { get; set; } = null!;
}