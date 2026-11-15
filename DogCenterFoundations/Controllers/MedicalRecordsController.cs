using DogCenterFoundations.Models.ViewModels;
using DogCenterFoundations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogCenterFoundations.Controllers;

[Authorize]
public class MedicalRecordsController : Controller
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordsController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }


    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var record = await _medicalRecordService.GetMedicalRecordDetailsByIdAsync(id);

            return View(record);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpGet]
    public async Task<IActionResult> Create(int dogId)
    {
        try
        {
            var model = await _medicalRecordService.GetMedicalRecordCreateViewModelAsync(dogId);

            return View(model);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MedicalRecordCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var createModel = await _medicalRecordService.GetMedicalRecordCreateViewModelAsync(model.DogId);

            model.DogName = createModel.DogName;

            return View(model);
        }

        await _medicalRecordService.CreateMedicalRecordAsync(model);

        TempData["SuccessMessage"] = "Medical record created successfully!";

        return RedirectToAction("Details", "Dogs", new { id = model.DogId });
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var record = await _medicalRecordService.GetMedicalRecordForEditAsync(id);

            return View(record);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MedicalRecordEditViewModel model)
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
            await _medicalRecordService.EditMedicalRecordAsync(model);

            TempData["SuccessMessage"] = "Medical record updated successfully!";

            return RedirectToAction("Details", "Dogs", new { id = model.DogId });
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
            int dogId = await _medicalRecordService.DeleteMedicalRecordAsync(id);

            TempData["SuccessMessage"] = "Medical record deleted successfully!";

            return RedirectToAction("Details", "Dogs", new { id = dogId });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

}
