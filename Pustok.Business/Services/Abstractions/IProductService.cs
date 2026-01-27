

namespace Pustok.Business.Services.Abstractions;

public interface IProductService
{
    Task CreateAsync(ProductCreateDto dto);
    Task UpdateAsync(ProductUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<ProductGetDto?> GetByIdAsync(Guid id);
    Task<List<ProductGetDto>> GetAllAsync();
}
