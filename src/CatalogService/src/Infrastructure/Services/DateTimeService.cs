using CatalogService.Application.Common.Interfaces;

namespace CatalogService.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
