namespace Wallet.Domain.Common;

public abstract class EntityBase
{
    public string CreatedBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? LastModifiedBy { get; set; } = "DefaultLastModifiedBy";
    public DateTime? LasteModeifiedDate { get; set; }
}