using AutoMapper;
using AutoMapper.QueryableExtensions;
using Seshat.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Seshat.Domain.Common;

namespace Seshat.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, 
            int pageNumber, 
            int pageSize) => 
            PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
            this IQueryable queryable,
            IConfigurationProvider configuration,
            CancellationToken cancellationToken = default) => 
            queryable.ProjectTo<TDestination>(configuration).ToListAsync(cancellationToken);
    }
}