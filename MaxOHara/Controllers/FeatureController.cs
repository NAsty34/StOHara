using Data.Repository.Interface;
using MaxOHara.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;

public class FeatureController:Controller
{
    private readonly IFeatureRepository _featureRepository;

    public FeatureController(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    [Authorize]
    [Route("/api/v1/check/{isCheck}")]
    [HttpPut]
    public async Task EditBooking (string isCheck)
    {
        var check = _featureRepository.GetBool();
        check.IsCheck = isCheck switch
        {
            "false" => false,
            "true" => true,
            _ => check.IsCheck
        };

        await _featureRepository.Edit(check.IsCheck);
    }
    
    [Route("/api/v1/check")]
    [HttpGet]
    public async Task<ResponseDto<string>> GetCheckBooking()
    {
        var check = _featureRepository.GetBool();
        return new ResponseDto<string>(check.IsCheck.ToString());
    }
}