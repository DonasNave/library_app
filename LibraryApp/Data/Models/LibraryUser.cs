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
public record LibraryUser : Document, ISeachable<LibraryUser>
{
    public override ObjectId Id { get; set; }
    public override DateTime CreatedAt { get; init; }
    public UserRole Role { get; set; } = UserRole.Anonymous;
    public string UserName { get; set; } = String.Empty;
    
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public List<BookBorrow> Borrows { get; set; }
    public List<Notification> Notifications { get; set; }
    public static FilterDefinition<LibraryUser> SearchFilter(string searchTerm)
    {
        throw new NotImplementedException();
    }                                                                
}

public class BookBorrow
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Author { get; init; }
    public DateTime BorrowedAt { get; init; }
    public int Coppies { get; init; }
}

public class Notification
{
    public string Id { get; init; }
    public DateTime Created { get; init; }
    public string Message { get; set; }
}