using LibraryApp.Data.Models;

namespace LibraryApp.Data.Migrations;

public class Migrator
{
    private readonly ICollection<Book> _dummyBooks = new List<Book>()
    {
        new()
        {
            Authors = new[]{"Bonifác Arnošt"}, Tags = new[]{"adventure", "drama"}, 
            Name = "O čem spím", AltName = "Nic moc", Description = "Fakt zajímavý dílo se mi zdá.", 
            Released = DateTime.Now, ISBN = "null", Publisher = "Černá hora spol."
        },
        new()
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
        new()
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
        new()
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
        new()
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
        new()
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
        new()
        {
            Authors = new []{ "Jarda Frantů" }, Tags = new[]{"sci-fi", "drama"}, 
            Name = "Vlna", AltName = "Spicy", Description = "Fantasy o obchodnících s vlnou.", 
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Černá hora spol."
            
        },
    };

    private readonly ICollection<LibraryUser> _dummyUsers = new List<LibraryUser>()
    {
        new ("admin", UserRole.Admin)
        {
            PasswordHash = "admin"
        },
        new ("user", UserRole.Customer)
        {
            PasswordHash = "user"
        }
    };

    private readonly IMongoRepository<LibraryUser> _libUsersRepository;
    private readonly IMongoRepository<Book> _booksRepository;

    public Migrator(IMongoRepository<LibraryUser> libUsersRepository, 
                    IMongoRepository<Book> booksRepository )
    {
        _libUsersRepository = libUsersRepository;
        _booksRepository = booksRepository;
    }


    public bool MigrateAll() => MigrateBooks() && MigrateUsers();

    private bool MigrateBooks()
    {
        try
        {
            _booksRepository.DeleteMany(book => true);
            _booksRepository.InsertMany( _dummyBooks);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    private bool MigrateUsers()
    {
        try
        {
            _libUsersRepository.DeleteMany(user => true);
            _libUsersRepository.InsertMany( _dummyUsers);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}

