using MongoDB.Bson;
using MongoDB.Driver;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers;

public class ListItemController
{
    // Set up MongoClient and connect to database
    // Define connection string
    private const string MONGO_DB_CONN_STRING = "mongodb+srv://benkisow:pMOyDWqwZ45iBcE3@to-do-list-app.javmepp.mongodb.net/?retryWrites=true&w=majority&appName=to-do-list-app";

    // Create a new MongoClient
    private static MongoClient mongoClient = new MongoClient(MONGO_DB_CONN_STRING);

    // Get the appropriate database
    private static IMongoDatabase database = mongoClient.GetDatabase("to-do-list-app");

    // Get the appropriate collection with that database, using the corresponding Model
    private static IMongoCollection<ListItem> collection = database.GetCollection<ListItem>("ListItems");

    // Method to get all list items
    public Task<List<ListItem>> GetAllListItems()
    {
        List<ListItem> listItems = collection.Find(new BsonDocument()).ToList();

        return Task.FromResult(listItems);
    }

    // Method to get a single list item
    public Task<ListItem> GetListItem(ObjectId id)
    {
        ListItem listItem = collection.Find(new BsonDocument()).ToList().Where(l => l.Id == id).ToList()[0];

        return Task.FromResult(listItem);
    }

    // Method to update a list item
    public Task<ReplaceOneResult> UpdateListItem(ListItem listItem)
    {
        FilterDefinition<ListItem> filter = Builders<ListItem>.Filter.Eq("_id", listItem.Id);
        ReplaceOneResult result = collection.ReplaceOne(filter, listItem);

        return Task.FromResult(result);
    }

    // Method to create a list item
    public Task<ListItem> AddListItem(ListItem listItem)
    {
        collection.InsertOne(listItem);

        return Task.FromResult(listItem);
    }

    // Method to delete a list item
    public Task<DeleteResult> DeleteListItem(ObjectId id)
    {
        FilterDefinition<ListItem> filter = Builders<ListItem>.Filter.Eq("_id", id);
        DeleteResult result = collection.DeleteOne(filter);

        return Task.FromResult(result);
    }
}