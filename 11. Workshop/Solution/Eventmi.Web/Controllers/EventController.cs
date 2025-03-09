using Eventmi.Core;
using Eventmi.Core.Contracts;
using Eventmi.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace Eventmi.Web.Controllers;

public class EventController : Controller
{
    private readonly IEventService _eventService;

    public EventController(IEventService service) => _eventService = service;

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(EventFormModel model)
    {
        if (!ModelState.IsValid) return View(model);

        ServiceResult<int> result = await _eventService.AddEventAsync(model);
        if (!result.Success)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(error.Key, error.Value);
            return View(model);
        }

        return RedirectToAction("All");
    }

    [HttpGet]
    public IActionResult All() => View(_eventService.GetAllEvents());

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", "Home");

        await _eventService.DeleteEvent(id.Value);
        return RedirectToAction("All");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", "Home");

        EventFormModel eventModel = await _eventService.GetEvent(id.Value);
        return View(eventModel);
    }

    [HttpGet]
    public async Task<ActionResult> Edit(int? id)
    {
        if (!id.HasValue) return RedirectToAction("Index", "Home");

        EventFormModel eventModel = await _eventService.GetEvent(id.Value);
        return View(eventModel);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(EventFormModel model)
    {
        if (!ModelState.IsValid) return View(model);

        ServiceResult<int> result = await _eventService.UpdateEvent(model);
        if (!result.Success)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(error.Key, error.Value);
            return View(model);
        }

        return RedirectToAction("All");
    }
}