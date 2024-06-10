using Domain.Abtractions.Entities;

namespace Domain.Entities
{
    public class Wish : Entity<Guid>
    {
        public Wish(Guid id, string name) { //, bool isActive) {
            Id = id;
            Name = name;
           // IsActive = isActive;
        }
        public string Name { get; set; }
        public virtual ICollection<WishTicket>? WishTickets { get; set; }
    }
}
