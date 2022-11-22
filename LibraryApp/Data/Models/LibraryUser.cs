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
    public List<BookBorrow> Borrows { get; set; } = new List<BookBorrow>();
    public List<Notification> Notifications { get; set; } = new List<Notification>();
}

public struct BookBorrow
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Author { get; init; }
    public DateTime BorrowedAt { get; init; }
    public int Coppies { get; init; }
}

public struct Notification
{
    public string Id { get; init; }
    public DateTime Created { get; init; }
    public string Message { get; set; }
}