using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBDemo;

public class PersonModel
{
    public ObjectId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
