using Data.Model.Entities;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class FeatureService:IFeatureService
{
    private readonly IFeatureRepository _featureRepository;

    public FeatureService(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    public FeatureEntity GetBool()
    {
        return _featureRepository.GetBool();
    }

    public Task Edit(bool check)
    {
        return _featureRepository.Edit(check);
    }
}