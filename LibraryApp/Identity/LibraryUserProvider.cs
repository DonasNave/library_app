using LibraryApp.Data.Models;
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

    public async Task<LibraryUser?> RegisterUser(string userName, string password)
    {
        var newUser = new LibraryUser(userName, UserRole.Customer)
        {
            PasswordHash = password
        };
        
        try
        {
            var db = _mongoClient.GetDatabase("library");
            var col = db.GetCollection<LibraryUser>(nameof(LibraryUser));
            await col.InsertOneAsync(newUser);
            return newUser;
        }
        catch
        {
            return null;
        }
    }
}