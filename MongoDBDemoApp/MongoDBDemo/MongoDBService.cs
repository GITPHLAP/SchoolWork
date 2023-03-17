using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo;

public class MongoDBService
{
    private IMongoDatabase mongoDatabase;

    public MongoDBService(IMongoDatabase mongoDatabase)
    {
        this.mongoDatabase = mongoDatabase;
    }

    public Task Insert<T>(T type, string collectionName)
    {
        var collection = mongoDatabase.GetCollection<T>(collectionName);
        return collection.InsertOneAsync(type);
    }

    public Task Upsert<T, TFieldValue>(T type, string collectionName, ObjectId filterKeyValue) where T : PersonModel
    {
        if (filterKeyValue == ObjectId.Empty)
        {

            filterKeyValue = ObjectId.GenerateNewId();
            type.Id = filterKeyValue;
        }
        var collection = mongoDatabase.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("_id", filterKeyValue);
        return collection.ReplaceOneAsync(filter, type, new ReplaceOptions { IsUpsert = true});

    }

    public async Task<List<T>> GetAllByKey<T, TFieldValue>(string collectionName, string filterKey, TFieldValue filterKeyValue)
    {
        var collection = mongoDatabase.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(filterKey, filterKeyValue);
        var results = await collection.FindAsync(filter);
        return results.ToList();
    }

    public async Task<T> GetOneByKey<T, TFieldValue>(string collectionName, string filterKey, TFieldValue filterKeyValue)
    {
        var collection = mongoDatabase.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(filterKey, filterKeyValue);
        var results = collection.Find<T>(filter);
        return results.First();
    }
}