using Data.Model.Lending;
using Data.Repository.Interface;

namespace Data.Repository;

public class AtmosphereLendingRepository:LendingRepository<AtmosphereLendingEntity>, IAtmosphereLendingRepository
{
    public AtmosphereLendingRepository(MaxOHaraContext context) : base(context)
    {
    }
}