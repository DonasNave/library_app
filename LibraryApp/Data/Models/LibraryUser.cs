using MongoDB.Bson;

namespace LibraryApp.Data.Models;
public enum UserRole
{
    Admin = 0,
    Customer = 1,
    Anonymous = 2,
}

[BsonCollection("LibraryUser")]
public sealed class LibraryUser : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt { get; }
    public UserRole Role { get; set; } = UserRole.Anonymous;
    public string UserName { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    
    public LibraryUser()
    {}
    
    public LibraryUser(string userName = "Anonymous user", UserRole userRole = UserRole.Anonymous)
    {
        UserName = userName;
        Role = userRole;
        CreatedAt = DateTime.Now;
    }

}