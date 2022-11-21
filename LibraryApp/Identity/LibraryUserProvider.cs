using LibraryApp.Data;
using LibraryApp.Data.Models;
using MongoDB.Driver;

namespace LibraryApp.Identity;

public class LibraryUserProvider
{
    private readonly IMongoRepository<LibraryUser> _libUsersRepository;

    public LibraryUserProvider(IMongoRepository<LibraryUser> libUsersRepository)
    {
        _libUsersRepository = libUsersRepository;
    }

    public async Task<LibraryUser?> GetUser(string userName)
    {
        try
        {
            var res = await _libUsersRepository.FindOneAsync(
                x => x.UserName == userName);
            return res;
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
            await _libUsersRepository.InsertOneAsync(newUser);
            return newUser;
        }
        catch
        {
            return null;
        }
    }
}