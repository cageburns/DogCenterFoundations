namespace DogCenterFoundations.Models.Entities;

public class Service
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Notes { get; set; }
}