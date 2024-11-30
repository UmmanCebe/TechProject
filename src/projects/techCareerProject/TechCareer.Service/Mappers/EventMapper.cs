using AutoMapper;
using TechCareer.Models.Dtos.Events.Response;
using TechCareer.Models.Dtos.Events.Request;
using TechCareer.Models.Entities;
using Core.Persistence.Extensions;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;


namespace TechCareer.Service.Mappers;

public  class EventMapper :Profile
{

    public EventMapper()
    {
        CreateMap<Event, EventResponseDto>();
        CreateMap<EventResponseDto, Event>();
        CreateMap<EventCreateRequestDto, Event>();
        CreateMap<EventUpdateRequestDto, Event>();
        CreateMap<Paginate<Event>, Paginate<EventResponseDto>>();
       
    }
}
