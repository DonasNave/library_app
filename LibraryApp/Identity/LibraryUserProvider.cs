using LibraryApp.Data;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Identity;

public class LibraryUserProvider
{
    private readonly IMongoRepository<LibraryUser> _libUsersRepository;
    private readonly PasswordHasher<LibraryUser> _passwordHasher;

    public LibraryUserProvider(IMongoRepository<LibraryUser> libUsersRepository, PasswordHasher<LibraryUser> passwordHasher)
    {
        _libUsersRepository = libUsersRepository;
        _passwordHasher = passwordHasher;
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
        LibraryUser newUser = new()
        {
            UserName = userName,
            Role = UserRole.Customer,
        };
        
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);
        
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

    public PasswordVerificationResult VerifyPassword(LibraryUser user, string password) =>
        _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
}