

namespace TechCareer.Models.Dtos.Events.Response;

public sealed record EventResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string ParticipationText { get; init; }
    public int CategoryId { get; init; }
}
