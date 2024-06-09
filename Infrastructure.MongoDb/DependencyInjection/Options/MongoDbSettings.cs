namespace Infrastructure.MongoDb.DependencyInjection.Options;

internal sealed record MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}