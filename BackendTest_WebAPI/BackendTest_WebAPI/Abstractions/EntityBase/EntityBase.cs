namespace BackendTest_WebAPI.Abstractions.EntityBase;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public virtual TKey Id { get; set; }
}
