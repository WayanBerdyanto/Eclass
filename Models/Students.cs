using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eclass.Models;

public class Students{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public String? Id {get; set; }

    [BsonElement("Nim")]
    public string Nim {get; set; } = null!;

    public string Name {get; set; } = null!;

    public string Address {get; set; } = null!;

    public string Email {get; set; } = null!;
}
