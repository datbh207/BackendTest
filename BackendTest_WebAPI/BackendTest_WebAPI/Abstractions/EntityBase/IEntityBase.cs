namespace BackendTest_WebAPI.Abstractions.EntityBase;

public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}
