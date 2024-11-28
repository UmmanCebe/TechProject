using Core.Persistence.Repositories;
using TechCareer.DataAccess.Contexts;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Concretes;

public sealed class EventRepository(BaseDbContext context) : EfRepositoryBase<Event, Guid, BaseDbContext>(context), IEventRepository
{
    public async Task<List<Event>> GetEventByCategory(int categoryId)
    {
        return await GetListAsync(@event => @event.CategoryId == categoryId);
    }
}
