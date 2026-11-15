using DogCenterFoundations.Models.ViewModels;

namespace DogCenterFoundations.Services;


public interface IDogService
{
    Task<IEnumerable<DogListViewModel>> GetAllDogsAsync();

    Task<DogDetailsViewModel> GetDogDetailsByIdAsync(int id);

    Task CreateDogAsync(DogCreateViewModel model);

    Task<DogEditViewModel> GetDogForEditAsync(int id);

    Task EditDogAsync(DogEditViewModel model);

    Task DeleteDogAsync(int id);
}