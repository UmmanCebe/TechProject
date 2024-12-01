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

    public async Task<InstructorResponseDto> AddAsync(InstructorCreateRequestDto dto)
    {
        Instructor addedInstructor = _mapper.Map<Instructor>(dto);
        Instructor instructor = await _instructorRepository.AddAsync(addedInstructor);
        InstructorResponseDto response = _mapper.Map<InstructorResponseDto>(instructor);
        return response;
    }

    public async Task<InstructorResponseDto> DeleteAsync(Guid id, bool permanent)
    {
        await _instructorBusinessRules.InstructorIdShouldBeExistsWhenSelected(id);

        Instructor? Instructor = await _instructorRepository.GetAsync(i => i.Id == id);

        Instructor deletedInstructor = await _instructorRepository.DeleteAsync(Instructor, permanent);
        InstructorResponseDto response = _mapper.Map<InstructorResponseDto>(deletedInstructor);
        return response;
    }

    public async Task<List<InstructorResponseDto>> GetAllAsync(Expression<Func<Instructor, bool>>? predicate = null, Func<IQueryable<Instructor>,
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
        List<InstructorResponseDto> response = _mapper.Map<List<InstructorResponseDto>>(instructorList);
        return response;
    }

    public async Task<InstructorResponseDto?> GetOneAsync(Expression<Func<Instructor, bool>> predicate, bool include = false, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        Instructor? instructor = await _instructorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        InstructorResponseDto response = _mapper.Map<InstructorResponseDto>(instructor);
        return response;
    }

    public async Task<Paginate<InstructorResponseDto>> GetPaginateAsync(Expression<Func<Instructor, bool>>? predicate = null, Func<IQueryable<Instructor>,
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
        Paginate<InstructorResponseDto> response = _mapper.Map<Paginate<InstructorResponseDto>>(instructorList);
        return response;
    }

    public async Task<InstructorResponseDto> UpdateAsync(InstructorUpdateRequestDto dto, Guid id)
    {
        await _instructorBusinessRules.InstructorIdShouldBeExistsWhenSelected(id);

        Instructor? instructor = await _instructorRepository.GetAsync(i => i.Id == id);
        instructor = _mapper.Map(dto, instructor);
        instructor.Id = id;
        Instructor updateInstructor = await _instructorRepository.UpdateAsync(instructor);

        InstructorResponseDto response = _mapper.Map<InstructorResponseDto>(updateInstructor);
        return response;
    }
}