namespace DogCenter.Models.Entities;

public class Owner
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public ICollection<Dog> Dogs { get; set; } = new List<Dog>();

    public string? Notes { get; set; }
}