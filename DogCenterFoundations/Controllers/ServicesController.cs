using DogCenterFoundations.Services;
using DogCenterFoundations.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogCenter.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ByType(string serviceType)
        {
            var services = await _serviceService.GetServicesByTypeAsync(serviceType);

            return View(services);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var service = await _serviceService.GetServiceDetailsByIdAsync(id);

                return View(service);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new ServiceCreateViewModel());
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _serviceService.CreateServiceAsync(model);

            TempData["SuccessMessage"] = "Service created successfully!";

            return RedirectToAction(nameof(ByType), new { serviceType = model.ServiceType });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var service = await _serviceService.GetServiceForEditAsync(id);

                return View(service);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _serviceService.EditServiceAsync(model);

                TempData["SuccessMessage"] = "Service updated successfully!";

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _serviceService.DeleteServiceAsync(id);

                TempData["SuccessMessage"] = "Service deleted successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
