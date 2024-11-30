using AutoMapper;
using TechCareer.Models.Dtos.Categories.RequestDto;
using TechCareer.Models.Dtos.Categories.ResponseDto;
using TechCareer.Models.Entities;


namespace TechCareer.Service.Mappers;

public class CategoryMapper : Profile
{

    public CategoryMapper()
    {
      
        CreateMap<Category, CategoryDto>();

        
        CreateMap<CreateCategoryRequestDto, Category>();
        CreateMap<UpdateCategoryRequestDto, Category>();
    }






}
