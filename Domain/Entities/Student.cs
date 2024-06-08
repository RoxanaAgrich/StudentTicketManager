using Domain.Abtractions.Common;
using Domain.Abtractions.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Student: Entity<Guid>, IAuditableEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }
        public  string? Gender { get; set; }
       
        public Guid? ClassId { get; set; }
        public virtual Class Class { get; set; } 
        public virtual Collection<Ticket> Tickets { get; set; }
    }
}
