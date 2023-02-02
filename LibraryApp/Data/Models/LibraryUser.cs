using System.Linq.Expressions;
using System.Text.RegularExpressions;
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
        var searchPattern = new BsonRegularExpression($"(.*){Regex.Escape(searchTerm)}(.*)");

        var nameField = new ExpressionFieldDefinition<LibraryUser, string>(user => user.Name);
        var authorField = new ExpressionFieldDefinition<LibraryUser, string>(user => user.Surname);
        var isbnField = new ExpressionFieldDefinition<LibraryUser, string>(user => user.UserName);
        
        bool? lookBanned = searchTerm.ToLower() switch
        {
            "banned" => true,
            "unbanned" => false,
            _ => null
        };
        
        bool? lookActive = searchTerm.ToLower() switch
        {
            "active" => true,
            "inactive" => false,
            _ => null
        };

        var nameFilter = Builders<LibraryUser>.Filter.Regex(nameField, searchPattern);
        var authorFilter = Builders<LibraryUser>.Filter.Regex(authorField, searchPattern);
        var isbnFilter = Builders<LibraryUser>.Filter.Regex(isbnField, searchPattern);
        var activeFilter = Builders<LibraryUser>.Filter.Where(user => user.Active == lookActive);
        var bannedFilter = Builders<LibraryUser>.Filter.Where(user => user.Banned == lookBanned);
        var adminFilter = Builders<LibraryUser>.Filter.Where(user => user.Role != UserRole.Admin);
            
        return (nameFilter | authorFilter | isbnFilter | activeFilter | bannedFilter) & adminFilter;
    }
}
public class Borrow
{
    public string Id { get; init; } = String.Empty;
    public string BookName { get; init; } = String.Empty;
    public DateTime Created { get; init; }
    public string Message { get; set; } = String.Empty;
}