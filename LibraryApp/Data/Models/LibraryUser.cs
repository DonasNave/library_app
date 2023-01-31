using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryApp.Data.Models;
public enum UserRole
{
    Admin = 0,
    Customer = 1,
    Anonymous = 2,
}

[BsonCollection("LibraryUser")]
public record LibraryUser : Document, ISearchable<LibraryUser>
{
    public override ObjectId Id { get; set; }
    public override DateTime CreatedAt { get; init; }
    public UserRole Role { get; set; } = UserRole.Anonymous;
    public string UserName { get; set; } = String.Empty;
    public bool Active { get; set; } = false;
    public bool Banned { get; set; } = false;
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public List<Borrow> Borrows { get; set; } = new();
    public List<Notification> Notifications { get; set; } = new();
    public static FilterDefinition<LibraryUser> SearchFilter(string searchTerm)
    {
        throw new NotImplementedException();
    }
}
public class Borrow
{
    public string Id { get; init; } = String.Empty;
    public string BookName { get; init; } = String.Empty;
    public DateTime Created { get; init; }
    public string Message { get; set; } = String.Empty;
}