using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.DataAccess.Repositories.Concretes;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public sealed class EventBusinessRules(IEventRepository _eventRepository): BaseBusinessRules
{

    public Task EventShouldBeExistsWhenSelected(Event? @event)
    {
        if (@event == null)
            throw new BusinessException(EventMessage.EventDontExists);
        return Task.CompletedTask;
    }


    public async Task EventIdShouldBeExistsWhenSelected(Guid id)
    {
        bool doesExist = await _eventRepository.AnyAsync(predicate: u => u.Id == id, enableTracking: false);
        if (doesExist is false)
            throw new BusinessException(EventMessage.EventDontExists);
    }


    public async Task EventTitleIsUnique(string title, CancellationToken cancellationToken)
    {
        var isPresent = await _eventRepository.AnyAsync(
            predicate: x => x.Title.Equals(title),
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        if (isPresent)
        {
            throw new BusinessException(EventMessage.EventTitleMustBeUnique); 
        }
    }

   
}
