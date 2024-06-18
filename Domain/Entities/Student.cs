using Domain.Abtractions.Common;
using Domain.Abtractions.Entities;

namespace Domain.Entities
{
    public class Student: Entity<Guid>, IAuditableEntity
    {
        public Student( string firstName, string lastName, string gender, Guid? classId) {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            ClassId = classId;
            IsActive = true;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        
        public DateTimeOffset CreatedOnUtc { get; set; }
        public DateTimeOffset? ModifiedOnUtc { get; set; }
        public  string Gender { get; set; }
       
        public Guid? ClassId { get; set; }
        public virtual Class Class { get; set; } 
        public virtual ICollection<Ticket>? Tickets { get; set; }
        
    }
}
