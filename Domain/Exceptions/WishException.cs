namespace Domain.Exceptions
{
    public static  class WishException 
    {
        public  class WishNotFoundException : NotFoundException
        {
            public WishNotFoundException(Guid Id)
                : base($"The Wish with Id {Id} was not found") { }
        }
        
    }
}
