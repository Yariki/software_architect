using System.Reflection;
using CartingService.Application.Interfaces;
using LiteDB;

namespace CartingService.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private LiteDatabase _db;
    
    public UnitOfWork()
    {   
        var name = "CartBucket.db";
        var dbFullPath = Path.Combine(Assembly.GetExecutingAssembly().Location,
            name);
        var connectionString = $"Filename={dbFullPath};";
        _db = new LiteDatabase(connectionString);
        CartRepository = new CartRepository(_db);

    }

    public void Dispose()
    {
        _db?.Dispose();
    }

    public ICartRepository CartRepository { get; }
}