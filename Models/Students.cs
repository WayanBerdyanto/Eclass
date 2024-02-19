using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Eclass.Models;

public class Students{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; }

    [BsonElement("Nim")]
    [JsonPropertyName("nim")]
    public string Nim{get; set; } = null!;

    public string Name{get; set; } = null!;

    public string Address{get; set; } = null!;

    public string Email{get; set; } = null!;
}
