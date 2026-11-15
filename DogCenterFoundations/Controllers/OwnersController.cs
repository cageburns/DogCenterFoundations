using DogCenterFoundations.Models.ViewModels;
using DogCenterFoundations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogCenterFoundations.Controllers;

[Authorize]
public class OwnersController : Controller
{
    private readonly IOwnerService _ownerService;

    public OwnersController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public async Task<IActionResult> Index()
    {
        var owners = await _ownerService.GetAllOwnersAsync();

        return View(owners);
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var owner = await _ownerService.GetOwnerDetailsByIdAsync(id);

            return View(owner);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OwnerCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _ownerService.CreateOwnerAsync(model);

        TempData["SuccessMessage"] = "Person created successfully!";

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var owner = await _ownerService.GetOwnerForEditAsync(id);

            return View(owner);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OwnerEditViewModel model)
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
            await _ownerService.EditOwnerAsync(model);

            TempData["SuccessMessage"] = "Person data updated successfully!";

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}