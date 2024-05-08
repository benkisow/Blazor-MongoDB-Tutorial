using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoListApp.Models;

public class ListItem
{
    // Constructor that will be used to create a new Task
    public ListItem(string content)
    {
        this.Content = content; // content is provided by the user
        this.IsCompleted = false; // IsCompleted always starts as false
    }

    // Auto-generated Id that will be created by MongoDB
    [BsonId]
    public ObjectId Id { get; set; }

    // Content of the task (Ex: "Pick up groceries")
    public string Content { get; set; }

    // Completion status of the task (true = Complete, false = Incomplete)
    public bool IsCompleted { get; set; }
}