using Microsoft.AspNetCore.Http;

namespace Pustok.Business.Dtos;

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}
