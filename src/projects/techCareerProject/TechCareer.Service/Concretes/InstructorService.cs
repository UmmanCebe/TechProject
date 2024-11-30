using AutoMapper;
using Core.Persistence.Extensions;
using System.Linq.Expressions;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Models.Dtos.Instructors.Response;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes;
public sealed class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IMapper _mapper;
    private readonly InstructorBusinessRules _instructorBusinessRules;

    public InstructorService(IInstructorRepository instructorRepository, IMapper mapper, InstructorBusinessRules instructorBusinessRules)
    {
        _instructorRepository = instructorRepository;
        _mapper = mapper;
        _instructorBusinessRules = instructorBusinessRules;
    }

    public async Task<Instructor> AddAsync(InstructorCreateRequestDto dto)
    {
        Instructor addedInstructor = _mapper.Map<Instructor>(dto);
        Instructor instructor = await _instructorRepository.AddAsync(addedInstructor);
        return instructor;
    }

    public async Task<Instructor> DeleteAsync(Instructor instructor, bool permanent = false)
    {
        await _instructorBusinessRules.InstructorShouldBeExistsWhenSelected(instructor);

        Instructor deletedInstructor = await _instructorRepository.DeleteAsync(instructor, permanent);
        return deletedInstructor;
    }

    public async Task<List<Instructor>> GetAllAsync(Expression<Func<Instructor, bool>>? predicate = null, Func<IQueryable<Instructor>,
        IOrderedQueryable<Instructor>>? orderBy = null, bool include = false,
        bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        List<Instructor> instructorList = await _instructorRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
            );
        return instructorList;
    }

    public async Task<Instructor?> GetOneAsync(Expression<Func<Instructor, bool>> predicate, bool include = false, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        Instructor? instructor = await _instructorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return instructor;
    }

    public async Task<Paginate<Instructor>> GetPaginateAsync(Expression<Func<Instructor, bool>>? predicate = null, Func<IQueryable<Instructor>,
        IOrderedQueryable<Instructor>>? orderBy = null, bool include = false, int index = 0, int size = 10, bool withDeleted = false,
        bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        Paginate<Instructor> instructorList = await _instructorRepository.GetPaginateAsync(
                predicate,
                orderBy,
                include,
                index,
                size,
                withDeleted,
                enableTracking,
                cancellationToken
            );
        return instructorList;
    }

    public async Task<InstructorResponseDto> UpdateAsync(InstructorUpdateRequestDto dto, Guid id)
    {
        await _instructorBusinessRules.InstructorIdShouldBeExistsWhenSelected(id);

        Instructor? instructor = await _instructorRepository.GetAsync(i => i.Id == id);
        instructor = _mapper.Map(dto, instructor);
        Instructor updateInstructor = await _instructorRepository.UpdateAsync(instructor);

        InstructorResponseDto response = _mapper.Map<InstructorResponseDto>(updateInstructor);
        return response;
    }
}