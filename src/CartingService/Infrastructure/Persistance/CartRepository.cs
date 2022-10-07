using System.Reflection;
using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;
using LiteDB;

namespace CartingService.Infrastructure.Persistance;

public class CartRepository : ICartRepository
{
    private LiteDatabase _db;
    private LiteCollection<Cart>? _dbSet;

    public CartRepository()
    {
        var name = "CartBucket.db";
        var dbFullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            name);
        var connectionString = $"Filename={dbFullPath};Connection=shared";
        _db = new LiteDatabase(connectionString);
       
        _dbSet = _db.GetCollection<Cart>() as LiteCollection<Cart>;
    }

    protected LiteCollection<Cart>? Set => _dbSet;

    public Cart GetCart(Guid id)
    {
        var cart = _dbSet.FindOne(c => c.Id == id);
        if (cart != null) 
            return cart;
        
        cart = new Cart()
        {
            Id = id
        };
        _dbSet.Insert(cart);
        return cart;
    }

    public void UpdateCart(Cart cart)
    {
        _dbSet?.Update(cart);
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}