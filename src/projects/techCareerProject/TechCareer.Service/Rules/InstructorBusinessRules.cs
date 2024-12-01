using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;
public class InstructorBusinessRules(IInstructorRepository _instructorRepository) : BaseBusinessRules
{
    public virtual Task InstructorShouldBeExistsWhenSelected(Instructor? instructor)
    {
        if (instructor is null)
            throw new BusinessException(InstructorMessages.InstructorDontExists);
        return Task.CompletedTask;
    }

    public async virtual Task InstructorIdShouldBeExistsWhenSelected(Guid id)
    {
        bool doesExist = await _instructorRepository.AnyAsync(predicate: i => i.Id == id, enableTracking: false);
        if (doesExist is false)
            throw new BusinessException(InstructorMessages.InstructorDontExists);
    }
}