
using LibraryApp.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

[BsonCollection("Notification")]
public record Notification : Document, ISearchable<Notification>
{
    public override ObjectId Id { get; set; }
    public override DateTime CreatedAt { get; init; }
    public NotificationAction Action { get; set; }
    public DateTime DueTo { get; set; }
    public string RefId { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;
    public string AssigneeId { get; set; } = String.Empty;
    public uint Priority;
    public bool Resolved { get; set; } = false;

    public static FilterDefinition<Notification> SearchFilter(string term)
    {
        throw new NotImplementedException();
    }
}

public enum NotificationAction
{
    ConfirmRegistration,
    ReturnBookReminder
}