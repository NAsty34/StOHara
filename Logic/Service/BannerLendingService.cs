using Data.Model.Lending;
using Data.Repository.Interface;

namespace Logic.Service;

public class BannerLendingService:LendingService<BannerLendingEntity>
{
    public BannerLendingService(IBannerLendingRepository lendingRepository) : base(lendingRepository)
    {
    }
}