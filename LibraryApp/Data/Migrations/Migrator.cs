using System.Reflection;
using System.Reflection.Metadata;
using LibraryApp.Data.Models;
using MongoDB.Driver;

namespace LibraryApp.Data.Migrations;

public class Migrator
{
    private readonly ICollection<Book> _dummyBooks = new List<Book>()
    {
        new Book 
        {
            Authors = new[]{"Bonifác Arnošt"}, Tags = new[]{"adventure", "drama"}, 
            Name = "O čem spím", AltName = "Nic moc", Description = "Fakt zajímavý dílo se mi zdá.", 
            Released = DateTime.Now, ISBN = "null", Publisher = "Černá hora spol."
        },
        new Book
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
    };
    
    private readonly IMongoDatabase _database;
    
    public Migrator(IMongoDatabase database)
    {
        _database = database;
    }


    public bool MigrateAll() => MigrateBooks();

    private bool MigrateBooks()
    {
        try
        {
            var col = _database.GetCollection<Book>(nameof(Book));
            col.DeleteMany(book => true);
            col.InsertMany( _dummyBooks); // TODO session handle
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

}

