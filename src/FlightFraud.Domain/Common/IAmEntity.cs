namespace FightFraud.Domain.Common
{
    public interface IAmEntity<T>
    {
        public T Id { get; set; }
    }
}