using Eclass.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Eclass.Services;

public class UserServices
{
    private bool IsValidGmailAddress(string email)
    {
        // Check if the email ends with @gmail.com
        return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase)
            || email.EndsWith("@si.ukdw.ac.id", StringComparison.OrdinalIgnoreCase);
    }

    private readonly IMongoCollection<Users> _usersCollection;

    public UserServices(IOptions<EclassDatabaseSettings> eclassDatabaseSettings)
    {
        var mongoClient = new MongoClient(eclassDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(eclassDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<Users>(
            eclassDatabaseSettings.Value.UsersCollectionName
        );
    }

    public async Task<List<Users>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<Users?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Users newUsers)
    {
        await _usersCollection.InsertOneAsync(newUsers);
    }

    public async Task UpdateAsync(string id, Users userStudents) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, userStudents);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
}
