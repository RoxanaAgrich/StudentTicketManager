namespace Infrastrucrure.DependencyInjection.Options
{
    public class JwtOption
    {
        public string User {  get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public  int ExpiredMin { get; set; }

    }
}
