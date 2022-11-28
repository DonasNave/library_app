namespace LibraryApp.Data.Models;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BsonCollectionAttribute : Attribute
{
    public string CollectionName { get; }

    public BsonCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class DocumentFormAttribute : Attribute
{
    public string[] AttributeNames { get; }

    public DocumentFormAttribute(string[] attrs)
    {
        AttributeNames = attrs;
    }
}