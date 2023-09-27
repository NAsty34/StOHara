using Data.Model;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class NewsService:BaseService<NewsEntity>, INewsService
{
    private readonly INewsRepository _newsRepository;
    public NewsService(IBaseRepository<NewsEntity?> baseRepository, INewsRepository newsRepository) : base(baseRepository)
    {
        _newsRepository = newsRepository;
    }
    public override async Task<PageModel<NewsEntity?>> GetPage(int? page, int? size)
    {
        return await _newsRepository.GetPage(page, size);
    }
}