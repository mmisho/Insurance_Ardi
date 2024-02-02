namespace Domain.Shared
{
    public abstract class BaseEntity<TKey>
    {
        public abstract TKey Id { get; set; }
    }
}
