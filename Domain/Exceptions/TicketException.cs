namespace Domain.Exceptions
{
    public static class TicketException
    {
        public class TicketNotFoundException : NotFoundException { 
            public TicketNotFoundException (Guid Id) : base($"the Ticket with id {Id} was not found") { }
        }
    }
}
