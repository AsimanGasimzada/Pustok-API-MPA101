namespace Pustok.Business.Dtos;

public class CategoryGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductGetDto> Products { get; set; } = [];
}