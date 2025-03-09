using Eventmi.Web.ViewModels.Town;

namespace Eventmi.Core.Contracts;

public interface ITownService
{
    Task<int> AddTownAsync(TownFormModel model);

    IQueryable<TownFormModel> GetAllTowns();

    public Task DeleteTown(int id);

    Task<TownFormModel> GetTown(int id);

    Task<ServiceResult<int>> UpdateTown(TownFormModel model);
}