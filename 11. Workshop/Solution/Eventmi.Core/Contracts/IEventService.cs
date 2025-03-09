using Eventmi.Web.ViewModels.Event;

namespace Eventmi.Core.Contracts;

public interface IEventService
{
    Task<ServiceResult<int>> AddEventAsync(EventFormModel model);

    IQueryable<EventFormModel> GetAllEvents();

    public Task DeleteEvent(int id);

    Task<EventFormModel> GetEvent(int id);

    Task<ServiceResult<int>> UpdateEvent(EventFormModel model);
}