using LibraryApp.Data;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Identity;

public class LibraryUserProvider
{
    private readonly IMongoRepository<LibraryUser> _libUsersRepository;
    private readonly IMongoRepository<Notification> _notificationsRepository;
    private readonly PasswordHasher<LibraryUser> _passwordHasher;
    public LibraryUser? CurrentUser { get; set; }
    
    public LibraryUser? GetLatestUser()
    {
        if (CurrentUser == null)
        {
            return null;
        }
        return _libUsersRepository.FindOneAsync(x => x.UserName == CurrentUser.UserName).Result;
    }
       

    public LibraryUserProvider(IMongoRepository<LibraryUser> libUsersRepository, IMongoRepository<Notification> notificationRepository, PasswordHasher<LibraryUser> passwordHasher)
    {
        _libUsersRepository = libUsersRepository;
        _notificationsRepository = notificationRepository;
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
        var notifyRegister = new Notification()
        {
            Title = $"Confirm '{userName}' registration",
            Action = NotificationAction.ConfirmRegistration,
            Message = $"Decide wether to allow registration of '{userName}' to go through",
            DueTo = DateTime.Now.AddDays(2),
            Priority = 1
        };

        try
        {
            await _libUsersRepository.InsertOneAsync(newUser);
            var insertion = await GetUser(userName);
            notifyRegister.RefId = insertion?.Id.ToString() ?? "unknown";
            await _notificationsRepository.InsertOneAsync(notifyRegister);
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