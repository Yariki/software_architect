using System.Reflection;
using LiteDB;

namespace CartingService.Infrastructure.Persistance;

public class ApplicationDbContext : IApplicationDbContext
{
    private readonly LiteDatabase _db;
    public ApplicationDbContext()
    {
        var name = "CartBucket.db";
        var dbFullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            name);
        var connectionString = $"Filename={dbFullPath};Connection=shared";
        //todo what the approach of the disposing are you using?
        _db = new LiteDatabase(connectionString);

    }

    public ILiteDatabase Database
    {
        get => _db;
    }
}