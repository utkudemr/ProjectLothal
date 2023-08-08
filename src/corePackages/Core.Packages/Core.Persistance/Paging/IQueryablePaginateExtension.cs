using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Paging
{
    public static class IQueryablePaginateExtension
    {
        public static async Task<Paginate<T>> ToPaginateAsync<T>(
            this IQueryable<T> source,
            int index,
            int size,
            CancellationToken cancellationToken
            )
        {
            int pageCount = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            List<T> result = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);
            Paginate<T> list = new()
            {
                Items = result,
                PageIndex = index,
                Count = pageCount,
                Size = size,
                Pages = (int)Math.Ceiling(pageCount / (double)pageCount)
            };
            return list;
        }
    }
}
