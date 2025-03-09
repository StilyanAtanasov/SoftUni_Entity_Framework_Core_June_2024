using Eventmi.Core.Contracts;
using Eventmi.Data.Common;
using Eventmi.Data.Models;
using Eventmi.Web.ViewModels.Event;
using Microsoft.IdentityModel.Tokens;

namespace Eventmi.Core;

public class EventService : IEventService
{
    private readonly IRepository _repository;

    public EventService(IRepository context) => _repository = context;

    public async Task<ServiceResult<int>> AddEventAsync(EventFormModel model)
    {
        ServiceResult<int> dateValidationResult = ValidateDate(model.Start, model.End, nameof(model.End));
        if (!dateValidationResult.Success) return dateValidationResult;

        (ServiceResult<int> result, Town? town) townValidationResult = await ValidateTownAsync(model.Town, nameof(model.Town));
        if (!townValidationResult.result.Success) return townValidationResult.result;

        Event newEvent = new()
        {
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Place = model.Place,
            Town = townValidationResult.town!
        };

        await _repository.AddAsync(newEvent);
        int rowsUpdated = await _repository.SaveChangesAsync();

        return ServiceResult<int>.Ok(rowsUpdated);
    }

    public IQueryable<EventFormModel> GetAllEvents() =>
        _repository
        .AllReadonly<Event>()
        .Select(e => new EventFormModel
        {
            Id = e.Id,
            Name = e.Name,
            Start = e.Start,
            End = e.End,
            Place = e.Place,
            Town = e.Town.Name
        });

    public async Task<ServiceResult<int>> UpdateEvent(EventFormModel model)
    {
        ServiceResult<int> dateValidationResult = ValidateDate(model.Start, model.End, nameof(model.End));
        if (!dateValidationResult.Success) return dateValidationResult;

        (ServiceResult<int> result, Town? town) townValidationResult = await ValidateTownAsync(model.Town, nameof(model.Town));
        if (!townValidationResult.result.Success) return townValidationResult.result;

        Event eventModel = (await _repository.GetByIdAsync<Event>(model.Id))!;

        eventModel.Name = model.Name;
        eventModel.Start = model.Start;
        eventModel.End = model.End;
        eventModel.Place = model.Place;
        eventModel.Town = townValidationResult.town!;

        _repository.Update(eventModel);
        int rowsUpdated = await _repository.SaveChangesAsync();

        return ServiceResult<int>.Ok(rowsUpdated);
    }

    public async Task DeleteEvent(int id)
    {
        _repository.Delete((await _repository.GetByIdAsync<Event>(id))!);
        await _repository.SaveChangesAsync();
    }

    public async Task<EventFormModel> GetEvent(int id)
    {
        Event e = (await _repository.FindByExpressionAsync<Event>(e => e.Id == id, e => e.Town))!;
        return new EventFormModel
        {
            Id = e.Id,
            Name = e.Name,
            Start = e.Start,
            End = e.End,
            Place = e.Place,
            Town = e.Town.Name
        };
    }

    private ServiceResult<int> ValidateDate(DateTime startDate, DateTime endDate, string field)
    {
        if (DateTime.Compare(startDate, endDate) > 0)
            return ServiceResult<int>.Fail(field, "End date cannot be earlier than Start date!");

        return ServiceResult<int>.Ok(0);
    }

    private async Task<(ServiceResult<int>, Town? town)> ValidateTownAsync(string townName, string field)
    {
        Town? town = await _repository.FindByExpressionAsync<Town>(t => t.Name == townName);
        if (town == null) return (ServiceResult<int>.Fail(field, "Invalid or unregistered town!"), null);

        return (ServiceResult<int>.Ok(0), town);
    }
}