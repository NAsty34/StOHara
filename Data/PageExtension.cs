using Data.Model;

namespace Data;

public static class PageExtension
{
    public static async Task<PageModel<T>> GetPage<T>(this IQueryable<T> queryable, int? page, int? size)
    {
        if (page is null or <= 0)
        {
            page = 1;
        }

        if (size is null or <= 0 or > 50)
        {
            size = 20;
        }

        IEnumerable<T> items = queryable.Skip((int)((page - 1) * size)).Take((int)size).ToList();
        var p = new PageModel<T>
        {
            Count = items.Count(),
            CurrentPage = (int)page,
            Size = (int)size,
            Items = items,
            Total = queryable.Count()
        };
        p.TotalPages = (int)Math.Ceiling(p.Total / (double)size);
        return p;
    }
}