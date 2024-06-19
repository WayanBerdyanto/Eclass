using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eclass.Models;

public class Users
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Username")]
    [JsonPropertyName("Username")]
    public string Username { get; set; } = null!;

    [BsonElement("Email")]
    public string? Email { get; set; }

    [BsonElement("Password")]
    public string Password { get; set; } = null!;
}
