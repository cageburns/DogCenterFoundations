using DogCenterFoundations.Models.ViewModels;

namespace DogCenter.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceListViewModel>> GetServicesByTypeAsync(string serviceType);

        Task<ServiceDetailsViewModel> GetServiceDetailsByIdAsync(int id);

        Task CreateServiceAsync(ServiceCreateViewModel model);

        Task<ServiceEditViewModel> GetServiceForEditAsync(int id);

        Task EditServiceAsync(ServiceEditViewModel model);

        Task DeleteServiceAsync(int id);
    }
}