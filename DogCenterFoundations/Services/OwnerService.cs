using DogCenterFoundations.Data;
using DogCenterFoundations.Models.Entities;
using DogCenterFoundations.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DogCenterFoundations.Services;

public class OwnerService : IOwnerService
{
    private readonly ApplicationDbContext _context;

    public OwnerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OwnerListViewModel>> GetAllOwnersAsync()
    {
        return await _context.Owners
            .Select(o => new OwnerListViewModel
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                PhoneNumber = o.PhoneNumber,
                Email = o.Email
            })
            .ToListAsync();
    }


    public async Task<OwnerDetailsViewModel> GetOwnerDetailsByIdAsync(int id)
    {
        var owner = await _context.Owners
             .Include(o => o.Dogs)
             .FirstOrDefaultAsync(o => o.Id == id);

        if (owner == null)
        {
            throw new ArgumentException("Person not found.");
        }

        return new OwnerDetailsViewModel
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            PhoneNumber = owner.PhoneNumber,
            Email = owner.Email,
            Notes = owner.Notes,
            Dogs = owner.Dogs
                .OrderBy(d => d.Name)
                .Select(d => new DogListViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Breed = d.Breed,
                    Birthday = d.Birthday,
                    OwnerName = $"{owner.FirstName} {owner.LastName}",
                    IsVip = d.IsVip
                })
                .ToList()
        };
    }


    public async Task CreateOwnerAsync(OwnerCreateViewModel model)
    {
        var owner = new Owner
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            Notes = model.Notes
        };

        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();
    }


    public async Task<OwnerEditViewModel> GetOwnerForEditAsync(int id)
    {
        var owner = await _context.Owners
            .FirstOrDefaultAsync(o => o.Id == id);

        if (owner == null)
        {
            throw new ArgumentException("Person not found.");
        }

        return new OwnerEditViewModel
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            PhoneNumber = owner.PhoneNumber,
            Email = owner.Email,
            Notes = owner.Notes
        };
    }


    public async Task EditOwnerAsync(OwnerEditViewModel model)
    {
        var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Id == model.Id);

        if (owner == null)
        {
            throw new ArgumentException("Person no longer exists.");
        }

        owner.FirstName = model.FirstName;
        owner.LastName = model.LastName;
        owner.PhoneNumber = model.PhoneNumber;
        owner.Email = model.Email;
        owner.Notes = model.Notes;

        await _context.SaveChangesAsync();
    }
}