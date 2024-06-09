using Domain.Abtractions.Entities;

namespace Domain.Entities
{
    public class Wish : Entity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
