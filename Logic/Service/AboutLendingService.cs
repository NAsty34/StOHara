using Data.Model.Lending;
using Data.Repository.Interface;

namespace Logic.Service;

public class AboutLendingService:LendingService<AboutLendingEntity>
{
    public AboutLendingService(IAboutLendingRepository lendingRepository) : base(lendingRepository)
    {
    }
}