using Microsoft.AspNetCore.Http;

namespace Pustok.Business.Dtos;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}
