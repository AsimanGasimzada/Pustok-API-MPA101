using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Services.Abstractions;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Abstractions;

namespace Pustok.Business.Services.Implementations;

internal class CategoryService(ICategoryRepository _repository, IMapper _mapper) : ICategoryService
{
    public async Task CreateAsync(CategoryCreateDto dto)
    {

        var isExistCategory = await _repository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower());

        if (isExistCategory)
            throw new AlreadyExistException();

        var category = _mapper.Map<Category>(dto);


        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            throw new NotFoundException();

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _repository.GetAll().Include(x => x.Products).ToListAsync();

        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        return dtos;
    }

    public async Task<CategoryGetDto> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            throw new NotFoundException();

        var dto = _mapper.Map<CategoryGetDto>(category);

        return dto;
    }

    public async Task UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(dto.Id);

        if (category is null)
            throw new NotFoundException();

        var isExistCategory = await _repository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower() && x.Id != dto.Id);

        if (isExistCategory)
            throw new AlreadyExistException();

        category = _mapper.Map<CategoryUpdateDto, Category>(dto, category);

        _repository.Update(category);
        await _repository.SaveChangesAsync();
    }
}
