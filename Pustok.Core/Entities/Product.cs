using Pustok.Core.Entities.Common;

namespace Pustok.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
}
