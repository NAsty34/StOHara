using Data.Model.Lending;
using Data.Repository.Interface;

namespace Logic.Service;

public class AtmosphereLendingService:LendingService<AtmosphereLendingEntity>
{
    public AtmosphereLendingService(IAtmosphereLendingRepository lendingRepository) : base(lendingRepository)
    {
    }
}