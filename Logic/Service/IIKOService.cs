using System.Net.Http.Json;
using Data.Model.Json;
using Data.Model.Options;
using Logic.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Logic.Service;

public class IikoService:IIikoService
{
    static readonly HttpClient Client = new();
    private readonly ForIIKO _iiko;

    public IikoService(IOptions<ForIIKO> iiko)
    {
        _iiko = iiko.Value;
    }

    public async Task<string> GetToken()
    {
        var api = new
        {
            apiLogin = _iiko.Login
        };
        var content = JsonConvert.SerializeObject(api);
        var responseAuth = await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/access_token", content);
        var token = await responseAuth.Content.ReadAsStringAsync();
        var postToken = JsonConvert.DeserializeObject<TokenDeserialize>(token);
        return postToken.token;
    }
}