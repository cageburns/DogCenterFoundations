using DogCenterFoundations.Data;
using DogCenterFoundations.Models.Entities;
using DogCenterFoundations.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DogCenterFoundations.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly ApplicationDbContext _context;

    public MedicalRecordService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<MedicalRecordListViewModel>> GetMedicalRecordsByDogIdAsync(int dogId)
    {
        return await _context.MedicalRecords
            .Where(m => m.DogId == dogId)
            .OrderByDescending(m => m.Date)
            .Select(m => new MedicalRecordListViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Date = m.Date
            })
            .ToListAsync();
    }


    public async Task<MedicalRecordDetailsViewModel> GetMedicalRecordDetailsByIdAsync(int id)
    {
        var record = await _context.MedicalRecords
            .Include(m => m.Dog)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (record == null)
        {
            throw new ArgumentException("Medical record not found.");
        }

        return new MedicalRecordDetailsViewModel
        {
            Id = record.Id,
            DogId = record.DogId,
            DogName = record.Dog.Name,
            RecordType = record.RecordType,
            Name = record.Name,
            Date = record.Date,
            Brand = record.Brand,
            NextDueDate = record.NextDueDate,
            Notes = record.Notes
        };
    }


    public async Task CreateMedicalRecordAsync(MedicalRecordCreateViewModel model)
    {
        var record = new MedicalRecord
        {
            DogId = model.DogId,
            RecordType = model.RecordType,
            Name = model.Name,
            Date = DateTime.Today,
            Brand = model.Brand,
            NextDueDate = model.NextDueDate,
            Notes = model.Notes
        };

        _context.MedicalRecords.Add(record);

        await _context.SaveChangesAsync();
    }


    public async Task<MedicalRecordEditViewModel> GetMedicalRecordForEditAsync(int id)
    {
        var record = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.Id == id);

        if (record == null)
        {
            throw new ArgumentException("Medical record not found");
        }

        return new MedicalRecordEditViewModel
        {
            Id = record.Id,
            DogId = record.DogId,
            RecordType = record.RecordType,
            Name = record.Name,
            Brand = record.Brand,
            NextDueDate = record.NextDueDate,
            Notes = record.Notes
        };
    }


    public async Task EditMedicalRecordAsync(MedicalRecordEditViewModel model)
    {
        var record = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.Id == model.Id);

        if (record == null)
        {
            throw new ArgumentException("Medical record not found");
        }

        record.RecordType = model.RecordType;
        record.Name = model.Name;
        record.Brand = model.Brand;
        record.NextDueDate = model.NextDueDate;
        record.Notes = model.Notes;

        await _context.SaveChangesAsync();
    }


    public async Task<int> DeleteMedicalRecordAsync(int id)
    {
        var record = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.Id == id);

        if (record == null)
        {
            throw new ArgumentException("Medical record not found");
        }

        int dogId = record.DogId;

        _context.MedicalRecords.Remove(record);

        await _context.SaveChangesAsync();

        return dogId;
    }

}

