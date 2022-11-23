using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LibraryApp.Data.Models;

[BsonCollection("Book")]
public record Book : Document, ISeachable<Book>
{
    public override ObjectId Id { get; set; }
    public override DateTime CreatedAt { get; init; }

    public List<String> Tags { get; set; } = new List<string>();
    public string Name { get; set; } = String.Empty;
    public string AltName { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime Released { get; init; }
    public string ISBN { get; init; } = String.Empty;
    public string Publisher { get; set; } = String.Empty;
    public uint Copies { get; set; } = 0;
    public uint Pages { get; set; } = 0;
    public byte[] ImageCover { get; set; } = new byte[] { };
    public byte[] ImageFront { get; set; } = new byte[] { };
    public Author Author { get; init; }
    
    public static FilterDefinition<Book> SearchFilter(string term)
    {
        var nameField = new ExpressionFieldDefinition<Book, string>(book => book.Name);
        var authorField = new ExpressionFieldDefinition<Book, string>(book => book.Author.Name);

        var nameFilter = Builders<Book>.Filter.Regex(nameField, "(.*)" + term + "(.*)");
        var authorFilter = Builders<Book>.Filter.Regex(authorField, "(.*)" + term + "(.*)");
        
        return nameFilter | authorFilter;
    }
}

public class Author
{
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public List<String> BooksWritten { get; set; }
}