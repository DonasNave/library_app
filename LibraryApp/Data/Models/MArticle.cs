using System.Buffers.Text;
using System.Drawing;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryApp.Data.Models;

public class MArticle<T> where T : IArticle
{
    public T Article { get; set; }
    public string Description { get; set; }
    
    public bool Active { get; set; }

    public MArticle(T article, string description = "Bez komentáře")
    {
        Article = article;
        Description = description;
    }
}


public class Book : IArticle
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = String.Empty;
    [BsonElement("Authors")] public string[] Authors { get; set; } = new[] { String.Empty, };
    [BsonElement("Tags")] public string[] Tags { get; set; } = new[] { String.Empty, };
    [BsonElement("Name")] public string Name { get; set; } = String.Empty;
    [BsonElement("AltName")] public string AltName { get; set; } = String.Empty;
    [BsonElement("Description")] public string Description { get; set; } = String.Empty;
    [BsonElement("Released")] public DateTime Released { get; set; }
    [BsonElement("ISBN")] public string ISBN { get; set; } = String.Empty;
    [BsonElement("Publisher")] public string Publisher { get; set; } = String.Empty;

    public bool Update()
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        throw new NotImplementedException();
    }
}

public struct Author
{
    public List<string> Names;
    public List<string> Surnames;
    public DateTime Birth;
    public string Bio;
    private Guid Id { get; init; }
}

public struct Publisher
{
    public string Name;
    public DateTime Created;
    public string Description; 
    private Guid Id { get; init; }
}