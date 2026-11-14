namespace DogCenterFoundations.Models.ViewModels;

public class DogListViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string OwnerName { get; set; } = null!;

    public bool IsVip { get; set; }
}