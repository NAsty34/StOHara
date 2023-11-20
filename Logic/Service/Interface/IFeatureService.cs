using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface IFeatureService
{
    public FeatureEntity GetBool();
    public Task Edit(bool check);
}