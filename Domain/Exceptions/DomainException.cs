namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string tile,string message) :
            base( message) 
            =>  Titile = tile;
       public string Titile { get; }
    }
}
