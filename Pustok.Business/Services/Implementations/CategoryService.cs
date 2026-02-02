using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pustok.Business.Exceptions;
using Pustok.Business.Services.Abstractions;
using Pustok.Core.Entities;
using Pustok.DataAccess.Repositories.Abstractions;

namespace Pustok.Business.Services.Implementations;

internal class CategoryService(ICategoryRepository _repository, IMapper _mapper) : ICategoryService
{
    public async Task<ResultDto> CreateAsync(CategoryCreateDto dto)
    {

        var isExistCategory = await _repository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower());

        if (isExistCategory)
            throw new AlreadyExistException();

        var category = _mapper.Map<Category>(dto);


        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return new ResultDto
        {
            IsSucced = true,
            Message = "Category created successfully"
        };
    }

    public async Task<ResultDto> DeleteAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            throw new NotFoundException();

        _repository.Delete(category);
        await _repository.SaveChangesAsync();

        return new ResultDto
        {
            IsSucced = true,
            Message = "Category deleted successfully"
        };
    }

    public async Task<ResultDto<List<CategoryGetDto>>> GetAllAsync()
    {
        var categories = await _repository.GetAll().Include(x => x.Products).ToListAsync();

        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        return new() { Data = dtos };
    }

    public async Task<ResultDto<CategoryGetDto>> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            throw new NotFoundException();

        var dto = _mapper.Map<CategoryGetDto>(category);

        return new() { Data = dto };
    }

    public async Task<ResultDto> UpdateAsync(CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(dto.Id);

        if (category is null)
            throw new NotFoundException();

        //{
        //id:1,
        //Name:Davud,
        //Description:"Salam 2"
        //
        //}

        //{
        //id:2,
        //Name:Nihat,
        //Description:""
        //
        //}

        var isExistCategory = await _repository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower() && x.Id != dto.Id);

        if (isExistCategory)
            throw new AlreadyExistException();

        category = _mapper.Map<CategoryUpdateDto, Category>(dto, category);

        _repository.Update(category);
        await _repository.SaveChangesAsync();

        return new ResultDto
        {
            IsSucced = true,
            Message = "Category updated successfully"
        };
    }
}
