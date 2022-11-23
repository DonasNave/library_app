using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LibraryApp.Data.Models;

public interface ISeachable<TDocument> where TDocument : IDocument
{
    static abstract FilterDefinition<TDocument> SearchFilter(string term);
} 

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }
    DateTime CreatedAt { get; }
}

public abstract record Document : IDocument
{
    public abstract ObjectId Id { get; set; }
    public abstract DateTime CreatedAt { get; init; }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BsonCollectionAttribute : Attribute
{
    public string CollectionName { get; }

    public BsonCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}