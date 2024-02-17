using Eclass.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Eclass.Services;

public class StudentsServices
{
    private readonly IMongoCollection<Students> _studentsCollection;

    public StudentsServices(
        IOptions<EclassDatabaseSettings> eclassDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            eclassDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            eclassDatabaseSettings.Value.DatabaseName);

        _studentsCollection = mongoDatabase.GetCollection<Students>(
            eclassDatabaseSettings.Value.StudentsCollectionName);
    }

    public async Task<List<Students>> GetAsync() =>
        await _studentsCollection.Find(_ => true).ToListAsync();

    public async Task<Students?> GetAsync(string id) =>
        await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Students newStudents) =>
        await _studentsCollection.InsertOneAsync(newStudents);

    public async Task UpdateAsync(string id, Students updatedStudents) =>
        await _studentsCollection.ReplaceOneAsync(x => x.Id == id, updatedStudents);

    public async Task RemoveAsync(string id) =>
        await _studentsCollection.DeleteOneAsync(x => x.Id == id);

    // Filter
    // public async Task AddToStudentListAsync(string id, string id2){
    //     FilterDefinition<Students> filter = Builders<Students>.Filter.Eq("id", id);
    //     UpdateDefinition<Students> update = Builders<Students>.Update.AddToSet<String>("id2", id2);

    //     await _studentsCollection.UpdateOneAsync(filter, update);
    //     return;
    // }
}