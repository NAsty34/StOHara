namespace Data.Model;

public class PageModel<T>
{
    public static PageModel<TEntity> Create<TEntity, TPEntity>(PageModel<TPEntity> pageModel, IEnumerable<TEntity> items)
    {
        return new PageModel<TEntity>()
        {
            Count = pageModel.Count,
            Size = pageModel.Size,
            Total = pageModel.Total,
            TotalPages = pageModel.TotalPages,
            CurrentPage = pageModel.CurrentPage,
            Items = items,
        };

    }
    /// <summary>
    /// общее количество в бд
    /// </summary>
    public int Total { get; set; } 
    /// <summary>
    /// количество эментов, которые возвращаем
    /// </summary>
    public int Count { get; set; } 
    /// <summary>
    /// размер страницы
    /// </summary>
    public int? Size { get; set; } 
    /// <summary>
    /// кол-во страниц
    /// </summary>
    public int? CurrentPage { get; set; }
    /// <summary>
    /// общее кол-во страниц
    /// </summary>
    public int TotalPages { get; set; } 
    /// <summary>
    /// элементы
    /// </summary>
    public IEnumerable<T>? Items { get; set; }
}