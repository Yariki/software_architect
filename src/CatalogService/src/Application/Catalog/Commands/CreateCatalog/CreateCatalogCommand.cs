﻿using AutoMapper;
using Catalog.Abstractions;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection.Catalog.Commands.CreateCatalog;

public class CreateCatalogCommand : IRequest<int>
{
    public string Name { get; set; }

    public string Image { get; set; }

    public int? CatalogId { get; set; }
    
}


public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, int>
{
    private IApplicationDbContext _applicationDbContext;
    private IMapper _mapper;
    
    public CreateCatalogCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = new CatalogService.Domain.Entities.Catalog()
        {
            Name = request.Name, Image = request.Image, CatalogId = request.CatalogId
        };
        
        _applicationDbContext.Catalogs.Add(catalog);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return catalog.Id;    
    }
}