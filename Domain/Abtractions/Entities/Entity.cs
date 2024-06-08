using System.ComponentModel.DataAnnotations;

namespace Domain.Abtractions.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; protected set; }
        public bool IsActive { get; protected set; }
    }
}
