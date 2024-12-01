namespace TechCareer.Models.Dtos.Instructors.Response;

public class InstructorResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string About { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
}
