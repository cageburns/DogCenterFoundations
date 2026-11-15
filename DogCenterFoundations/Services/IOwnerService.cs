using DogCenterFoundations.Models.ViewModels;

namespace DogCenterFoundations.Services;

public interface IOwnerService
{
    Task<IEnumerable<OwnerListViewModel>> GetAllOwnersAsync();

    Task<OwnerDetailsViewModel> GetOwnerDetailsByIdAsync(int id);

    Task CreateOwnerAsync(OwnerCreateViewModel model);

    Task<OwnerEditViewModel> GetOwnerForEditAsync(int id);

    Task EditOwnerAsync(OwnerEditViewModel model);
}