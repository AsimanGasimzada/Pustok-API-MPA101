

namespace Pustok.Business.Services.Abstractions;

public interface IProductService
{
    Task<ResultDto> CreateAsync(ProductCreateDto dto);
    Task<ResultDto> UpdateAsync(ProductUpdateDto dto);
    Task<ResultDto> DeleteAsync(Guid id);
    Task<ResultDto<ProductGetDto>> GetByIdAsync(Guid id);
    Task<ResultDto<List<ProductGetDto>>> GetAllAsync();
}
