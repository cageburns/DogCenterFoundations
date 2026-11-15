using DogCenterFoundations.Data;
using DogCenterFoundations.Models.Entities;
using DogCenterFoundations.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DogCenterFoundations.Services;

public class DogService : IDogService
{
    private readonly ApplicationDbContext _context;

    public DogService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DogListViewModel>> GetAllDogsAsync()
    {
        return await _context.Dogs
            .Select(d => new DogListViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Breed = d.Breed,
                Birthday = d.Birthday,
                OwnerName = d.Owner.FirstName + " " + d.Owner.LastName,
                IsVip = d.IsVip
            })
            .ToListAsync();
    }


    public async Task<DogDetailsViewModel> GetDogDetailsByIdAsync(int id)
    {
        var dog = await _context.Dogs
            .Include(d => d.Owner)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dog == null)
        {
            throw new ArgumentException("Dog not found.");
        }

        return new DogDetailsViewModel
        {
            Id = dog.Id,
            Name = dog.Name,
            Breed = dog.Breed,
            Birthday = dog.Birthday,
            MicrochipId = dog.MicrochipId,
            RegistrationDate = dog.RegistrationDate,
            IsVip = dog.IsVip,
            Notes = dog.Notes,
            OwnerId = dog.OwnerId,
            OwnerName = dog.Owner.FirstName + " " + dog.Owner.LastName
        };
    }


    public async Task CreateDogAsync(DogCreateViewModel model)
    {
        var dog = new Dog
        {
            Name = model.Name,
            Breed = model.Breed,
            Birthday = model.Birthday,
            MicrochipId = model.MicrochipId,
            OwnerId = model.OwnerId,
            IsVip = model.IsVip,
            Notes = model.Notes,
            RegistrationDate = DateTime.Today,
        };

        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();
    }


    public async Task<DogEditViewModel> GetDogForEditAsync(int id)
    {
        var dog = await _context.Dogs
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dog == null)
        {
            throw new ArgumentException("Dog not found.");
        }

        return new DogEditViewModel
        {
            Id = dog.Id,
            Name = dog.Name,
            Breed = dog.Breed,
            Birthday = dog.Birthday,
            MicrochipId = dog.MicrochipId,
            OwnerId = dog.OwnerId,
            IsVip = dog.IsVip,
            Notes = dog.Notes
        };
    }

    public async Task EditDogAsync(DogEditViewModel model)
    {
        var dog = await _context.Dogs
            .FirstOrDefaultAsync(d => d.Id == model.Id);

        if (dog == null)
        {
            throw new ArgumentException("Dog no longer exists.");
        }

        dog.Name = model.Name;
        dog.Breed = model.Breed;
        dog.Birthday = model.Birthday;
        dog.MicrochipId = model.MicrochipId;
        dog.OwnerId = model.OwnerId;
        dog.IsVip = model.IsVip;
        dog.Notes = model.Notes;

        await _context.SaveChangesAsync();
    }


    public async Task DeleteDogAsync(int id)
    {
        var dog = await _context.Dogs
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dog == null)
        {
            throw new ArgumentException("Dog no longer exists.");
        }

        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();
    }
}