using System.Buffers.Text;
using System.Drawing;

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
    public List<Author> Authors;
    public List<string> Tags;
    public string Name;
    public string AltName;
    public string Description;
    public DateTime Released;
    public string ISBN;
    public Publisher Publisher;
    private Guid Id { get; init; }

    public Book(List<Author> authors, List<string> tags, string name, string altName, string description, 
        DateTime released, string isbn, Publisher publisher, Guid id)
    {
        Authors = authors;
        Tags = tags;
        Name = name;
        AltName = altName;
        Description = description;
        Released = released;
        ISBN = isbn;
        Publisher = publisher;
        Id = id;
    }

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