﻿using CartingService.Application.Interfaces;
using CartingService.Domain.Entities;
using LiteDB;

namespace CartingService.Infrastructure.Persistance;

public class CartRepository : ICartRepository
{
    private LiteCollection<Cart>? _dbSet;

    public CartRepository(IApplicationDbContext context)
    {
        _dbSet = context.Database.GetCollection<Cart>() as LiteCollection<Cart>;
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
}