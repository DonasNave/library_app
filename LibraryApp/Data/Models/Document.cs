using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LibraryApp.Data.Models;

public interface ISearchable<TDocument> where TDocument : IDocument
{
    static abstract FilterDefinition<TDocument> SearchFilter(string term);
} 

public interface IFormable
{
    Dictionary<string, (object, Type)> FormAttributes();
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