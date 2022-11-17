using MongoDB.Driver;

namespace LibraryApp.Areas.Identity;

public class LibraryUserProvider
{
    private MongoClient _mongoClient;

    public LibraryUserProvider(MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<LibraryUser?> GetUser(string UserName)
    {
        var db = _mongoClient.GetDatabase("library");
        var col = db.GetCollection<LibraryUser>(nameof(LibraryUser));
        var res = await col.FindAsync(x => x.UserName == UserName);
        return res.Current is null ? null : res.Current.FirstOrDefault();
    }

}