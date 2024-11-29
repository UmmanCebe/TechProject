using TechCareer.Models.Enums;

namespace TechCareer.Models.Dtos.VideoEducation.ResponseDto;

public class VideoEducationResponse
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public double TotalHour { get; init; }
    public bool IsCertified { get; init; }
    public Level Level { get; init; }
    public string ImageUrl { get; init; }
    public string ProgrammingLanguage { get; init; }






}
