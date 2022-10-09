using LiteDB;

namespace CartingService.Application.Interfaces;

public interface IApplicationDbContext
{
    public LiteDatabase Database { get; }
}