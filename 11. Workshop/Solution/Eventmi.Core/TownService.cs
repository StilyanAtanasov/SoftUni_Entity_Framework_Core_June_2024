using Eventmi.Core.Contracts;
using Eventmi.Data.Common;
using Eventmi.Data.Models;
using Eventmi.Web.ViewModels.Event;
using Eventmi.Web.ViewModels.Town;

namespace Eventmi.Core;

public class TownService : ITownService
{
	private readonly IRepository _repository;

	public TownService(IRepository context) => _repository = context;

	public async Task<int> AddTownAsync(TownFormModel model)
	{
		Town town = new()
		{
			Name = model.Name,
		};

		await _repository.AddAsync(town);
		int rowsUpdated = await _repository.SaveChangesAsync();

		return rowsUpdated;
	}

	public IQueryable<TownFormModel> GetAllTowns() => _repository
		.AllReadonly<Town>()
		.Select(t => new TownFormModel
		{
			Id = t.Id,
			Name = t.Name
		});

	public async Task DeleteTown(int id)
	{
		_repository.Delete((await _repository.GetByIdAsync<Town>(id))!);
		await _repository.SaveChangesAsync();
	}

	public async Task<TownFormModel> GetTown(int id)
	{
		Town t = (await _repository.FindByExpressionAsync<Town>(t => t.Id == id, t => t.Events))!;
		return new TownFormModel
		{
			Id = t.Id,
			Name = t.Name,
			Events = t.Events
				.Select(e => new EventDisplayModel
				{
					Id = e.Id,
					Name = e.Name,
					End = e.End,
				})
				.ToHashSet()
		};
	}

	public async Task<ServiceResult<int>> UpdateTown(TownFormModel model)
	{
		Town eventModel = (await _repository.GetByIdAsync<Town>(model.Id))!;
		eventModel.Name = model.Name;

		_repository.Update(eventModel);
		int rowsUpdated = await _repository.SaveChangesAsync();

		return ServiceResult<int>.Ok(rowsUpdated);
	}
}