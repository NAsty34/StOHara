using Data.Model.Entities;

namespace Data.Repository.Interface;

public interface IFeatureRepository
{
    public FeatureEntity GetBool();
    public Task Edit(FeatureEntity t);
}