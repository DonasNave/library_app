using System.Linq.Expressions;
using LibraryApp.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryApp.Data;

public interface IMongoRepository<TDocument> where TDocument : IDocument, ISearchable<TDocument>
{
    IQueryable<TDocument> AsQueryable();

    IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression);
    
    IEnumerable<TDocument> FilterBy(
        FilterDefinition<TDocument> filterDefinition);

    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

    Task<IAsyncCursor<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression);
    
    Task<IAsyncCursor<TDocument>> FilterByAsync(FilterDefinition<TDocument> filterDefinition);

    IEnumerable<TDocument> SearchFor(string term);
    
    Task<IAsyncCursor<TDocument>> SearchForAsync(string term);

    TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    TDocument FindById(string id);
    
    TDocument FindById(ObjectId id);

    Task<TDocument> FindByIdAsync(string id);

    void InsertOne(TDocument document);

    Task InsertOneAsync(TDocument document);

    void InsertMany(ICollection<TDocument> documents);

    Task InsertManyAsync(ICollection<TDocument> documents);

    void ReplaceOne(TDocument document);

    Task ReplaceOneAsync(TDocument document);

    void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

    Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    void DeleteById(string id);

    Task DeleteByIdAsync(string id);

    void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

    Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
}