using Eventmi.Core;
using Eventmi.Core.Contracts;
using Eventmi.Web.ViewModels.Event;
using Eventmi.Web.ViewModels.Town;
using Microsoft.AspNetCore.Mvc;

namespace Eventmi.Web.Controllers;

public class TownController : Controller
{
	private readonly ITownService _townService;

	public TownController(ITownService service) => _townService = service;

	[HttpGet]
	public IActionResult Add() => View();

	[HttpPost]
	public async Task<IActionResult> Add(TownFormModel townModel)
	{
		if (!ModelState.IsValid) return View(townModel);

		await _townService.AddTownAsync(townModel);

		return RedirectToAction("Index", "Home");
	}

	[HttpGet]
	public IActionResult All() => View(_townService.GetAllTowns());

	[HttpGet]
	public async Task<IActionResult> Details(int? id)
	{
		if (!id.HasValue) return RedirectToAction("Index", "Home");

		TownFormModel eventModel = await _townService.GetTown(id.Value);
		return View(eventModel);
	}

	[HttpPost]
	public async Task<IActionResult> Delete(int? id)
	{
		if (!id.HasValue) return RedirectToAction("Index", "Home");

		await _townService.DeleteTown(id.Value);
		return RedirectToAction("All");
	}

	[HttpGet]
	public async Task<ActionResult> Edit(int? id)
	{
		if (!id.HasValue) return RedirectToAction("Index", "Home");

		TownFormModel eventModel = await _townService.GetTown(id.Value);
		return View(eventModel);
	}

	[HttpPost]
	public async Task<ActionResult> Edit(TownFormModel model)
	{
		if (!ModelState.IsValid) return View(model);

		ServiceResult<int> result = await _townService.UpdateTown(model);
		if (!result.Success)
		{
			foreach (var error in result.Errors) ModelState.AddModelError(error.Key, error.Value);
			return View(model);
		}

		return RedirectToAction("All");
	}
}