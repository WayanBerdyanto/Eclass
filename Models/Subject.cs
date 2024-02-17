using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eclass.Models;

public class Subject
{
    public string? Id { get; set; }

    public string KodeSubject { get; set; } = null!;

    public string AmountSks { get; set; } = null!;

    public string TypeOfCourse { get; set; } = null!;

}
