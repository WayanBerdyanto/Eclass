using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eclass.Models;

public class Lecturer{

    public string? Id {get; set;}

    public string Nip {get; set;} = null!;

    public string Name {get; set;} = null!;

    public string Address {get; set;} = null!;
}