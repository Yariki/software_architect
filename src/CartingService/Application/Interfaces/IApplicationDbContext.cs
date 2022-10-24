using LiteDB;

namespace CartingService.Application.Interfaces;

public interface IApplicationDbContext
{
    public ILiteDatabase Database { get; }
}