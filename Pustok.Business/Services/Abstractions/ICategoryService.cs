namespace Pustok.Business.Services.Abstractions;

public interface ICategoryService
{
    Task CreateAsync(CategoryCreateDto dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(CategoryUpdateDto dto);
    Task<CategoryGetDto> GetByIdAsync(Guid id);
    Task<List<CategoryGetDto>> GetAllAsync();
}
