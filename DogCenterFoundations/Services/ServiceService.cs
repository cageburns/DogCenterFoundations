using DogCenterFoundations.Data;
using DogCenterFoundations.Models.Entities;
using DogCenterFoundations.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DogCenter.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;

        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceListViewModel>> GetServicesByTypeAsync(string serviceType)
        {
            return await _context.Services
                .Where(s => s.ServiceType == serviceType)
                .OrderBy(s => s.ServiceName)
                .Select(s => new ServiceListViewModel
                {
                    Id = s.Id,
                    ServiceName = s.ServiceName,
                    ServiceType = s.ServiceType
                })
                .ToListAsync();
        }

        public async Task<ServiceDetailsViewModel> GetServiceDetailsByIdAsync(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                throw new ArgumentException("Service not found.");
            }

            return new ServiceDetailsViewModel
            {
                Id = service.Id,
                ServiceName = service.ServiceName,
                ServiceType = service.ServiceType,
                Price = service.Price,
                Description = service.Description
            };
        }

        public async Task CreateServiceAsync(ServiceCreateViewModel model)
        {
            var service = new Service
            {
                ServiceName = model.ServiceName,
                ServiceType = model.ServiceType,
                Price = model.Price,
                Description = model.Description
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceEditViewModel> GetServiceForEditAsync(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                throw new ArgumentException("Service not found");
            }

            return new ServiceEditViewModel
            {
                Id = service.Id,
                ServiceName = service.ServiceName,
                ServiceType = service.ServiceType,
                Price = service.Price,
                Description = service.Description
            };
        }

        public async Task EditServiceAsync(ServiceEditViewModel model)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == model.Id);

            if (service == null)
            {
                throw new ArgumentException("Service not found");
            }

            service.ServiceName = model.ServiceName;
            service.ServiceType = model.ServiceType;
            service.Price = model.Price;
            service.Description = model.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                throw new ArgumentException("Service not found.");
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}