using LibraryApp.Areas.Identity;
using MongoDB.Driver;

namespace LibraryApp.Identity;

public class LibraryUserProvider
{
    private readonly MongoClient _mongoClient;

    public LibraryUserProvider(MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<LibraryUser?> GetUser(string userName)
    {
        try
        {
            var db = _mongoClient.GetDatabase("library");
            var col = db.GetCollection<LibraryUser>(nameof(LibraryUser));
            var res = await col.FindAsync(x => x.UserName == userName);
            return res.Single();
        }
        catch
        {
            return null;
        }
    }

}