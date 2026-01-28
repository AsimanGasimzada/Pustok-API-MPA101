namespace Pustok.Business.Services.Abstractions;

public interface ICategoryService
{
    Task<ResultDto> CreateAsync(CategoryCreateDto dto);
    Task<ResultDto> DeleteAsync(Guid id);
    Task<ResultDto> UpdateAsync(CategoryUpdateDto dto);
    Task<ResultDto<CategoryGetDto>> GetByIdAsync(Guid id);
    Task<ResultDto<List<CategoryGetDto>>> GetAllAsync();
}
