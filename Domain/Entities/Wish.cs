using Domain.Abtractions.Common;
using Domain.Abtractions.Entities;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class Wish:Entity<Guid>
    {
        public string WishName { get; set; }
        public virtual Collection<Ticket>? Tickets { get; set; }
    }
}
