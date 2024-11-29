using AutoMapper;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Mappers;

public class VideoEducationMapper : Profile
{
    public VideoEducationMapper()
    {
        CreateMap<VideoEducation, VideoEducationResponse>();
        CreateMap<VideoEducationCreateRequest, VideoEducation>();
        CreateMap<VideoEducationUpdateRequest, VideoEducation>();
    }
}