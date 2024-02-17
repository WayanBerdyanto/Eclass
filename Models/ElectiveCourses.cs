using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eclass.Models;

public class ElectiveCourses{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } 

    public string Nim {get; set; } = null!;

    public string KodeSubject {get; set; } = null!;

}