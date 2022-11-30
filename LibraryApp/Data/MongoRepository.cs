using System.Linq.Expressions;
using LibraryApp.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryApp.Data;

public class MongoRepository<TDocument> : IMongoRepository<TDocument>
    where TDocument : IDocument, ISearchable<TDocument>
{
    private readonly IMongoCollection<TDocument> _collection;

    public MongoRepository(IMongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute) documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
    }

    public IQueryable<TDocument> AsQueryable() 
        => _collection.AsQueryable();

    public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression) 
        => _collection.Find(filterExpression).ToEnumerable();

    public IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterDefinition)
        => _collection.Find(filterDefinition).ToEnumerable();
    
    public async Task<IAsyncCursor<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression) 
        => await _collection.FindAsync(filterExpression);

    public async Task<IAsyncCursor<TDocument>> FilterByAsync(FilterDefinition<TDocument> filterDefinition)
        => await _collection.FindAsync(filterDefinition);
    
    public IEnumerable<TDocument> SearchFor(string term)
        => _collection.Find(TDocument.SearchFilter(term)).ToEnumerable();
    
    public async Task<IAsyncCursor<TDocument>> SearchForAsync(string term)
        => await _collection.FindAsync(TDocument.SearchFilter(term));

    public IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
            => _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();

    public TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        => _collection.Find(filterExpression).FirstOrDefault();

    public Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        => Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());

    public TDocument FindById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        return _collection.Find(filter).SingleOrDefault();
    }
    
    public TDocument FindById(ObjectId id)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
        return _collection.Find(filter).SingleOrDefault();
    }

    public Task<TDocument> FindByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefaultAsync();
        });
    }

    public void InsertOne(TDocument document)
        => _collection.InsertOne(document);

    public Task InsertOneAsync(TDocument document)
        => Task.Run(() => _collection.InsertOneAsync(document));

    public void InsertMany(ICollection<TDocument> documents)
        => _collection.InsertMany(documents);

    public async Task InsertManyAsync(ICollection<TDocument> documents)
        => await _collection.InsertManyAsync(documents);

    public void ReplaceOne(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        _collection.FindOneAndReplace(filter, document);
    }

    public async Task ReplaceOneAsync(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        await _collection.FindOneAndReplaceAsync(filter, document);
    }

    public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        => _collection.FindOneAndDelete(filterExpression);

    public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        => Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));

    public void DeleteById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        _collection.FindOneAndDelete(filter);
    }

    public Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        => _collection.DeleteMany(filterExpression);

    public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        => Task.Run(() => _collection.DeleteManyAsync(filterExpression));
}