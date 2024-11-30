using AutoMapper;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Models.Dtos.Instructors.Response;
using TechCareer.Models.Dtos.Users;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Mappers;

public class InstructorMapper : Profile
{
    public InstructorMapper()
    {
        CreateMap<Instructor,InstructorCreateRequestDto>();
        CreateMap<Instructor, InstructorUpdateRequestDto>();
        CreateMap<Instructor, InstructorResponseDto>();
        CreateMap<Paginate<Instructor>, Paginate<InstructorResponseDto>>();
    }
}