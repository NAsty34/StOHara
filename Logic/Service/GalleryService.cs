using Data.Model;
using Data.Model.Entities;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class GalleryService:BaseService<GalleryEntity>, IGalleryService
{
    private readonly IGalleryRepository _galleryRepository;
    public GalleryService(IBaseRepository<GalleryEntity?> baseRepository, IGalleryRepository galleryRepository) : base(baseRepository)
    {
        _galleryRepository = galleryRepository;
    }
    public override async Task<PageModel<GalleryEntity?>> GetPage(int? page, int? size)
    {
        return await _galleryRepository.GetPage(page, size);
    }
}