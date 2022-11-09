namespace CatalogService.Domain.Entities;

public enum OutBoxStatus
{
    New,
    Pushing,
    Pushed,
    Failed,
}