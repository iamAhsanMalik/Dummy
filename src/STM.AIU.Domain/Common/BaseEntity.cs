namespace STM.AIU.Domain.Common;

public abstract class BaseEntity<PrimaryKeyType>
{
    public PrimaryKeyType? Id { get; set; }
}