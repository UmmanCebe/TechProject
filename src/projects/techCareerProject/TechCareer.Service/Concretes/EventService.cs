

using AutoMapper;
using Core.Persistence.Extensions;
using System.Linq.Expressions;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.DataAccess.Repositories.Concretes;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes;

public sealed class EventService(IEventRepository _eventRepository, EventBusinessRules _eventBusinessRules, IMapper mapper) : IEventService
{

    public async Task<EventResponseDto> GetAsync(Expression<Func<EventResponseDto, bool>> predicate, bool include = false, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
       Event? @event = await _eventRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);

        EventResponseDto response = mapper.Map<EventResponseDto>(@event);
        return response;

    }


    public async Task<Paginate<EventResponseDto>> GetPaginateAsync(Expression<Func<Event, bool>>? predicate = null, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null, bool include = false, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        Paginate<Event> eventList = await _eventRepository.GetPaginateAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken);

        Paginate<EventResponseDto> response = mapper.Map<Paginate<EventResponseDto>>(eventList);
        return response;
    }


    public async Task<List<EventResponseDto>> GetListAsync(Expression<Func<EventResponseDto, bool>>? predicate = null, Func<IQueryable<Event>, IOrderedQueryable<Event>>? orderBy = null, bool include = false, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
       List<EventResponseDto> EventList=await _eventRepository.GetListAsync(predicate, orderBy, include, withDeleted, enableTracking,cancellationToken); 
        
        List<EventResponseDto> response=mapper.Map<List<EventResponseDto>>(EventList);
        return response;
    }



    public async Task<EventResponseDto> AddAsync(EventCreateRequestDto request)
    {
        Event @event= mapper.Map<Event>(request);
        Event addedEvent= await _eventRepository.AddAsync(@event);

        EventResponseDto response=mapper.Map<EventResponseDto>(addedEvent);
        return response;
    }


    public async Task<EventResponseDto> UpdateAsync(Guid id, EventUpdateRequestDto request)
    {
        await _eventBusinessRules.EventIdShouldBeExistsWhenSelected(id);

        Event @event = (await _eventRepository.GetAsync(u => u.Id == id))!;

        @event = mapper.Map(request, @event);

        @event.Id = id;

        Event updatedEvent = await _eventRepository.UpdateAsync(@event);

        EventResponseDto response = mapper.Map<EventResponseDto>(updatedEvent);

        return response;


    }

    public async Task<EventResponseDto> DeleteAsync(Guid id, bool permanent = false)
    {
        await _eventBusinessRules.EventIdShouldBeExistsWhenSelected(id);
        Event eventToBeDeleted = (await _eventRepository.GetAsync(u => u.Id == id))!;
        Event deletedEvent = await _eventRepository.DeleteAsync(eventToBeDeleted, permanent);
        EventResponseDto response=mapper.Map<EventResponseDto>(deletedEvent);
        return response ;

    }
}
