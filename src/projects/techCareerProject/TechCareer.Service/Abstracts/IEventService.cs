using Core.Persistence.Extensions;
using Core.Security.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;

public interface IEventService
{
    Task<EventResponseDto> GetAsync(
        Expression<Func<EventResponseDto, bool>> predicate,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
         CancellationToken cancellationToken = default
        );


    Task<Paginate<EventResponseDto>> GetPaginateAsync(Expression<Func<Event, bool>>? predicate = null,
     Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
        bool include = false,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<List<EventResponseDto>> GetListAsync(Expression<Func<EventResponseDto, bool>>? predicate = null,
        Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null,
       bool include = false,
       bool withDeleted = false,
       bool enableTracking = true,
    CancellationToken cancellationToken = default);


    Task<EventResponseDto> AddAsync(EventCreateRequestDto request);
    Task<EventResponseDto> UpdateAsync(Guid id,EventUpdateRequestDto request);
    Task<EventResponseDto> DeleteAsync(Guid id, bool permanent = false);

}
  

