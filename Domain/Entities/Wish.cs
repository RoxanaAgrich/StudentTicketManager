using Domain.Abtractions.Entities;
using System.Reflection.Metadata;

namespace Domain.Entities
{
    public class Wish : Entity<Guid>
    {
        public Wish(Guid id, string name) {
            Id = id;
            Name = name;
        }
        public string Name { get; set; }
        public virtual ICollection<WishTicket> WishTickets { get; set; }
    }
}
