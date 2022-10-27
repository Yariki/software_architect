namespace CatalogService.Application.Common.Models;

public class EntitiesCollection<TEntity> : BaseDto where TEntity : class
{
    public EntitiesCollection(IEnumerable<TEntity> collection)
    {
        Data = collection;
    }
    
    public IEnumerable<TEntity> Data { get; private set; } = new List<TEntity>();
}
