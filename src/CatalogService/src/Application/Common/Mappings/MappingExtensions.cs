using AutoMapper;
using AutoMapper.QueryableExtensions;
using CatalogService.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}
