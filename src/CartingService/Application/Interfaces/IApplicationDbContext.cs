using LiteDB;

namespace CartingService.Application.Interfaces;

public interface IApplicationDbContext
{
    //todo in your opinion is it ok to have reference to LiteDB in the Application layer?
    public LiteDatabase Database { get; }
}