namespace LibraryApp.Data;

public interface IMongoDbSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
}

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
}