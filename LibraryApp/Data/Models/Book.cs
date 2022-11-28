using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryApp.Data.Models;

[BsonCollection("Book")]
[DocumentForm(new []
{
    nameof(Name), nameof(AltName), nameof(Description), nameof(Released), nameof(ISBN),
    nameof(Publisher), nameof(Copies),
})]
public record Book : Document, ISearchable<Book>, IFormable
{
    public override ObjectId Id { get; set; }
    public override DateTime CreatedAt { get; init; }

    public List<String> Tags { get; set; } = new List<string>();
    public string Name { get; set; } = String.Empty;
    public string AltName { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime Released { get; set; }
    public string ISBN { get; set; } = String.Empty;
    public string Publisher { get; set; } = String.Empty;
    public uint Copies { get; set; } = 0;
    public uint Pages { get; set; } = 0;
    public byte[] ImageCover { get; set; } = new byte[] { };
    public byte[] ImageFront { get; set; } = new byte[] { };
    public Author Author { get; init; }
    
    public static FilterDefinition<Book> SearchFilter(string term)
    {
        var searchPattern = new BsonRegularExpression($"(.*){Regex.Escape(term)}(.*)");
        
        var nameField = new ExpressionFieldDefinition<Book, string>(book => book.Name);
        var authorField = new ExpressionFieldDefinition<Book, string>(book => book.Author.Name);
        var isbnField = new ExpressionFieldDefinition<Book, string>(book => book.ISBN);

        var nameFilter = Builders<Book>.Filter.Regex(nameField, searchPattern);
        var authorFilter = Builders<Book>.Filter.Regex(authorField, searchPattern);
        var isbnFilter = Builders<Book>.Filter.Regex(isbnField, searchPattern);

        return nameFilter | authorFilter | isbnFilter;
    }

    public Dictionary<string, (object, Type)> FormAttributes()
    {
        var docForm = typeof(Book).GetCustomAttributes<DocumentFormAttribute>().FirstOrDefault();
        var dict = new Dictionary<string, (object, Type)>();
        
        if (docForm is not null)
        {
            foreach (var attr in docForm.AttributeNames)
            {
                var prop = GetType().GetProperty(attr);
                dict[attr] = (prop.GetValue(this), prop.GetType());
            }
        }
        
        return dict;
    }
}

public class Author
{
    public string Name { get; set; }
    public DateTime Birth { get; set; }
    public List<String> BooksWritten { get; set; }
}