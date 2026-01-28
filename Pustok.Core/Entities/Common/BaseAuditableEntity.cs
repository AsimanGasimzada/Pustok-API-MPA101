namespace Pustok.Core.Entities.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}