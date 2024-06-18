namespace Domain.Exceptions
{
    public static class StudentException 
    {
        public class StudentNotFoundException :NotFoundException { 
            public StudentNotFoundException(Guid Id):
                base($"The student with {Id} was not found")
            { }
        }
    }
}
