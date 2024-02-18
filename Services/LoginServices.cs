using DnsClient.Protocol;
using Eclass.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Eclass.Services;

public class LoginServices
{
    private readonly IMongoCollection<Users> _usersCollection;

    public LoginServices(IOptions<EclassDatabaseSettings> eclassDatabaseSettings)
    {
        var mongoClient = new MongoClient(eclassDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(eclassDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<Users>(
            eclassDatabaseSettings.Value.UsersCollectionName
        );
    }

    public async Task<List<Users>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    // public async Task<List<Users>> AuthLogin(string username) =>
    //     await _usersCollection.Find(x => x.Username == username).ToListAsync();

    public async Task<Users?> AuthLogin(string username) =>
        await _usersCollection.Find(x => x.Username == username).FirstOrDefaultAsync();
}
