using Data.Model.Lending;
using Data.Repository.Interface;

namespace Data.Repository;

public class BannerLendingRepository:LendingRepository<BannerLendingEntity>, IBannerLendingRepository
{
    public BannerLendingRepository(MaxOHaraContext context) : base(context)
    {
    }
}