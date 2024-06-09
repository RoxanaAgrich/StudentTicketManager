using Domain.Abtractions.Common;
using Domain.Abtractions.Entities;

namespace Domain.Entities
{
    public class Ticket : Entity<Guid>, IAuditableEntity
    {
        public Guid WishId { get; set; }
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Wish> Wishes { get; set; }

    }
}
