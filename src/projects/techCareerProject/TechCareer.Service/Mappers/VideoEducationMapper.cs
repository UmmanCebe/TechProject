using AutoMapper;
using Core.Persistence.Extensions;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Mappers;

public class VideoEducationMapper : Profile
{
    public VideoEducationMapper()
    {
        CreateMap<VideoEducation, VideoEducationResponse>()
            .ForMember(response => response.InstructorName,
                        memberOptions => memberOptions.MapFrom(videoEducation => videoEducation.Instructor.Name));
        CreateMap<Paginate<VideoEducation>, Paginate<VideoEducationResponse>>();
        CreateMap<VideoEducationCreateRequest, VideoEducation>();
        CreateMap<VideoEducationUpdateRequest, VideoEducation>();
    }
}