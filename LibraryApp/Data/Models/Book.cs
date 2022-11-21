using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryApp.Data.Models;

[BsonCollection("Book")]
public class Book : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt { get; }
    [BsonElement("Authors")] public string[] Authors { get; set; } = new[] { String.Empty, };
    [BsonElement("Tags")] public string[] Tags { get; set; } = new[] { String.Empty, };
    [BsonElement("Name")] public string Name { get; set; } = String.Empty;
    [BsonElement("AltName")] public string AltName { get; set; } = String.Empty;
    [BsonElement("Description")] public string Description { get; set; } = String.Empty;
    [BsonElement("Released")] public DateTime Released { get; set; }
    [BsonElement("ISBN")] public string ISBN { get; set; } = String.Empty;
    [BsonElement("Publisher")] public string Publisher { get; set; } = String.Empty;
}