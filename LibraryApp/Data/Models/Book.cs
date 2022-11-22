using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryApp.Data.Models;

[BsonCollection("Book")]
public record Book : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt { get; }
    public List<String> Tags { get; set; } = new List<string>();
    public string Name { get; set; } = String.Empty;
    public string AltName { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime Released { get; init; }
    public string ISBN { get; init; } = String.Empty;
    public string Publisher { get; set; } = String.Empty;
    public uint Copies { get; set; } = 0;
    public Author Author { get; init; }
}

public struct Author
{
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public List<String> BooksWritten { get; set; }
}