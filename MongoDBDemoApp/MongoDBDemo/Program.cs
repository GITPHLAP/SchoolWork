
using Amazon.Runtime.Internal.Auth;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo;

string connectionString = "mongodb://indoedb:Kode1234!@philipvpn.ignorelist.com:27017";
string databaseName = "simple_db";
string collectionName = "people";

var client  = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<PersonModel>(collectionName);

//var person = new PersonModel { FirstName = "Jens", LastName = "Jensen" };



MongoDBService dBService = new MongoDBService(db);

//await dBService.Insert<PersonModel>(person, collectionName);

var person2s = await dBService.GetAllByKey<PersonModel, string>(collectionName, "FirstName", "Jonas");
var person2 = person2s.Any() ? person2s[0] : new PersonModel(); 
person2.FirstName = "Ole";
person2.LastName = "Olsen";

//var person2 = new PersonModel { FirstName = "Lars", LastName = "Jensen" };

await dBService.Upsert<PersonModel, string>(person2, collectionName, person2.Id);


//var resultss = await dBService.GetAllByKey<PersonModel, string>(collectionName, "FirstName", "Jens");


//foreach (var result in resultss)
//{
//    Console.WriteLine($" {result.FirstName} {result.LastName}");
//}




//await collection.InsertOneAsync(person);


//var results = await collection.FindAsync(_ => true);

//foreach(var result in results.ToList())
//{
//    Console.WriteLine($"{result.Id}: {result.FirstName} {result.LastName}");
//}