using Data.Model;

namespace Data;

public static class PageExtension
{
    public static async Task<PageModel<T>> GetPage<T>(this IQueryable<T> queryable, int? page, int? size)
    {
        if (page == null || page <= 0)
        {
            page = 1;
        }

        if (size == null || size <= 0 || size > 50)
        {
            size = 20;
        }

        IEnumerable<T> items = queryable.Skip((int)((page - 1) * size)).Take((int)size).ToList();
        PageModel<T> p = new PageModel<T>();
        p.Count = items.Count();
        p.CurrentPage = (int)page;
        p.Size = (int)size;
        p.Items = items;
        p.Total = queryable.Count();
        p.TotalPages = (int)Math.Ceiling(p.Total / (double)size);
        return p;
    }
}