using Domain.Abtractions.Common;
using Domain.Abtractions.Entities;

namespace Domain.Entities
{
    public class Class:Entity<Guid>,IAuditableEntity
    {
        public string ClassName { get; set; }
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }
        public List<Student>? Students { get; set; }
    }
}
