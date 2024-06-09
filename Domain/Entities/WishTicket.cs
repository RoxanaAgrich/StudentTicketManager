using Domain.Abtractions.Common;
using Domain.Abtractions.Entities;

namespace Domain.Entities
{
    public class WishTicket : Entity<Guid>, IAuditableEntity
    {
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public Guid WishId { get; set; }
        public virtual Wish Wish { get; set; }

        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }
    }
}
