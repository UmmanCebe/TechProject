using Core.Persistence.Repositories;
using System.Linq.Expressions;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Abstracts;

public interface IEventRepository : IAsyncRepository<Event, Guid>
{
    Task<Event?> GetAsync(Expression<Func<EventResponseDto, bool>> predicate, bool include, bool withDeleted, bool enableTracking, CancellationToken cancellationToken);
    Task<List<Event>> GetEventsByCategory(int categoryId);
    Task<List<EventResponseDto>> GetListAsync(Expression<Func<EventResponseDto, bool>>? predicate, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy, bool include, bool withDeleted, bool enableTracking, CancellationToken cancellationToken);
}
