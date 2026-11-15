using DogCenterFoundations.Models.ViewModels;
using DogCenterFoundations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogCenterFoundations.Controllers;

[Authorize]
public class DogsController : Controller
{

    private readonly IDogService _dogService;
    private readonly IMedicalRecordService _medicalRecordService;

    public DogsController(IDogService dogService, IMedicalRecordService medicalRecordService)
    {
        _dogService = dogService;
        _medicalRecordService = medicalRecordService;
    }



    public async Task<IActionResult> Index()
    {
        var dogs = await _dogService.GetAllDogsAsync();

        return View(dogs);
    }



    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var dog = await _dogService.GetDogDetailsByIdAsync(id);

            dog.MedicalRecords = await _medicalRecordService.GetMedicalRecordsByDogIdAsync(id);

            return View(dog);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = await _dogService.GetDogCreateViewModelAsync();

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DogCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var createModel = await _dogService.GetDogCreateViewModelAsync();

            model.Owners = createModel.Owners;

            return View(model);
        }

        await _dogService.CreateDogAsync(model);

        TempData["SuccessMessage"] = "Dog record created successfully!";

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var dog = await _dogService.GetDogForEditAsync(id);

            return View(dog);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DogEditViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        try
        {
            if (!ModelState.IsValid)
            {
                var editModel = await _dogService.GetDogForEditAsync(id);

                model.Owners = editModel.Owners;

                return View(model);
            }

            await _dogService.EditDogAsync(model);

            TempData["SuccessMessage"] = "Dog updated successfully!";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _dogService.DeleteDogAsync(id);

            TempData["SuccessMessage"] = "Dog record deleted successfully!";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}
