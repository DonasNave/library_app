using System;
using System.Collections.Generic;
using System.Text.Json;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace LibraryApp.Data.Migrations;

public class Migrator
{
    
    private readonly IMongoRepository<LibraryUser> _libUsersRepository;
    private readonly IMongoRepository<Book> _booksRepository;

    public Migrator(IMongoRepository<LibraryUser> libUsersRepository,
                    IMongoRepository<Book> booksRepository)
    {
        _libUsersRepository = libUsersRepository;
        _booksRepository = booksRepository;
    }

    public void SetupIndex()
    {
        var userBuilder = Builders<LibraryUser>.IndexKeys;
        var indexModel = new CreateIndexModel<LibraryUser>(userBuilder.Text("Borrows.Created"));
        indexModel.Options.ExpireAfter = TimeSpan.FromDays(6);
        
        _libUsersRepository.SetupIndex(indexModel);
    }

    public void Export(RepositoryType type)
    {
        var json = type switch
        {
            RepositoryType.LibraryUser => _libUsersRepository.ExportRepository(),
            RepositoryType.Book => _booksRepository.ExportRepository(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        var serializedFile = JsonSerializer.Serialize(json);
        File.WriteAllText($"./Data/Documents/{type.ToString()}export{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.json", serializedFile);
    }
    
    public void Import(RepositoryType type, string filePath)
    {
        //Load file from filePath
        var json = JsonDocument.Parse(File.ReadAllText($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}/LibraryApp/LibraryApp/Data/Documents/{filePath}"));
        switch (type)
        {
            case RepositoryType.Book:
                _booksRepository.ImportRepository(json);
                break;
            
            case RepositoryType.LibraryUser:
                _libUsersRepository.ImportRepository(json);
                break;
            default: throw new NotImplementedException();
        }
    }
}

