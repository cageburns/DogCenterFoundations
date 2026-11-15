using DogCenterFoundations.Models.ViewModels;

namespace DogCenterFoundations.Services;

public interface IMedicalRecordService  
{
    Task<IEnumerable<MedicalRecordListViewModel>> GetMedicalRecordsByDogIdAsync(int dogId);

    Task<MedicalRecordDetailsViewModel> GetMedicalRecordDetailsByIdAsync(int id);

    Task<MedicalRecordCreateViewModel> GetMedicalRecordCreateViewModelAsync(int dogId);

    Task CreateMedicalRecordAsync(MedicalRecordCreateViewModel model);

    Task<MedicalRecordEditViewModel> GetMedicalRecordForEditAsync(int id);

    Task EditMedicalRecordAsync(MedicalRecordEditViewModel model);

    Task<int> DeleteMedicalRecordAsync(int id);
}
