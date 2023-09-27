using Data.Model.Lending;
using Data.Repository.Interface;

namespace Data.Repository;

public class AboutLendingRepository:LendingRepository<AboutLendingEntity>, IAboutLendingRepository
{
    public AboutLendingRepository(MaxOHaraContext context) : base(context)
    {
    }
}