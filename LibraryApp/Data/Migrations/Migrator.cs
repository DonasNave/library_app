using System;
using System.Collections.Generic;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Data.Migrations;

public class Migrator
{
    private readonly ICollection<Book> _dummyBooks = new List<Book>()
    {
        new()
        {
            Author = new (){ Name="Tim of Books", Birth = DateTime.Now.AddYears(-55), BooksWritten = new (){"O čem spím"}},
            Tags = new(){"adventure", "drama"}, Name = "O čem spím", AltName = "Nic moc", Copies = 5, Pages = 321,
            Description = "Truly magnificent work of literature.", Released = DateTime.Now, ISBN = "null", Publisher = "Blackrock com."
        },
        new()
        {
            Author = new (){ Name="Hubert Reed", Birth = DateTime.Now.AddYears(-65), BooksWritten = new (){"Wool"}},
            Tags = new(){"sci-fi", "drama"}, Name = "Wool", AltName = "Spicy", Description = "Fantasy about wool sellers.",
            Released = DateTime.Now, ISBN = "561919BFG", Publisher = "Blackrock com.", Copies = 6, Pages = 245,

        }
    };

    private readonly ICollection<LibraryUser> _dummyUsers = new List<LibraryUser>()
    {
        new()
        {
            UserName = "admin",
            Role = UserRole.Admin,
            PasswordHash = "admin"
        },
        new ()
        {
            UserName = "user",
            Role = UserRole.Customer,
            PasswordHash = "user"
        }
    };

    private readonly IMongoRepository<LibraryUser> _libUsersRepository;
    private readonly IMongoRepository<Book> _booksRepository;
    private readonly PasswordHasher<LibraryUser> _passwordHasher;

    public Migrator(IMongoRepository<LibraryUser> libUsersRepository,
                    IMongoRepository<Book> booksRepository, PasswordHasher<LibraryUser> passwordHasher)
    {
        _libUsersRepository = libUsersRepository;
        _booksRepository = booksRepository;
        _passwordHasher = passwordHasher;

        foreach (var user in _dummyUsers)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
        }
    }


    public bool MigrateAll() => MigrateBooks() && MigrateUsers();

    private bool MigrateBooks()
    {
        try
        {
            _booksRepository.DeleteMany(book => true);
            _booksRepository.InsertMany(_dummyBooks);
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
            _libUsersRepository.InsertMany(_dummyUsers);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}

