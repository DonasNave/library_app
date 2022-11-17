using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Areas.Identity;
public enum UserRole
{
    Admin = 0,
    Customer = 1,
    Anonymous = 2,
}
public sealed class LibraryUser : IdentityUser
{
    public UserRole Role { get; set; } = UserRole.Anonymous;
    
    public LibraryUser()
    {}
    
    public LibraryUser(string userName = "Anonymous user", UserRole userRole = UserRole.Anonymous) : base(userName)
    {
        UserName = userName;
        Role = userRole;
    }
}