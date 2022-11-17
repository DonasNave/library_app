using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Areas.Identity;
public enum UserRole
{
    Admin = 0,
    Customer = 1,
    Anonymous = 2,
}
public class LibraryUser : IdentityUser
{
    public UserRole Role { get; set; } = UserRole.Anonymous;

    public LibraryUser(string userName = "Anonymous user", UserRole userRole = UserRole.Anonymous) : base(userName)
    {
        Role = userRole;
    }
}