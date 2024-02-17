using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

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
    public string Email { get; set; } = null!;

    [BsonElement("Password")]
    public string Password { get; set; } = null!;

}
