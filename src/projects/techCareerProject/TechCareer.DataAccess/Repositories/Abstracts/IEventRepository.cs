using Core.Persistence.Repositories;
using System.Linq.Expressions;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Abstracts;

public interface IEventRepository : IAsyncRepository<Event, Guid>
{
    Task<List<Event>> GetEventsByCategory(int categoryId);
}
