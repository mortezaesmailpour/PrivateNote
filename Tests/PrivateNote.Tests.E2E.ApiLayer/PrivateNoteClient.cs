using Newtonsoft.Json;
using PrivateNote.Api.Dto;
using PrivateNote.Api.Dto.Responses;
using PrivateNote.Service.Contract;
using System.Text;

namespace PrivateNote.Tests.E2E.ApiLayer;
internal class PrivateNoteClient
{
    private readonly string _baseUrl;
    public PrivateNoteClient()
    {
        _baseUrl = "http://localhost/api/Auth/";
    }

    public async Task<bool> SignUpAsync(string userName, string password)
    {
        var signUpData = new Dictionary<string, string>();
        signUpData.Add("UserName", userName);
        signUpData.Add("Password", password);
        HttpClient client = new();
        var json = JsonConvert.SerializeObject(signUpData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_baseUrl + "SignUp", content);
        //if (!response.IsSuccessStatusCode)
        //    _logger.LogError("SignUp Not Success response : " + response.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<string?> SignInAsync(string userName, string password)
    {
        var signInData = new Dictionary<string, string>();
        signInData.Add("UserName", userName);
        signInData.Add("Password", password);
        HttpClient client = new();
        var json = JsonConvert.SerializeObject(signInData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_baseUrl + "SignIn", content);
        //if (!response.IsSuccessStatusCode)
        //    _logger.LogError("Incorrect Username or Password");
        string responseString = await response.Content.ReadAsStringAsync();
        var signInResponse = JsonConvert.DeserializeObject<SignInResponse>(responseString);
        //if (signInResponse is null)
        //    _logger.LogError("Incorrect Json format in SignIn response");
        return signInResponse?.Token;
    }

    public async Task<IUser?> WhoAmI(string token)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        var response = await client.GetAsync(_baseUrl + "HowAmI");
        //if (!response.IsSuccessStatusCode)
        //    _logger.LogError("token is not valid");
        string responseString = await response.Content.ReadAsStringAsync();
        var userInfo = JsonConvert.DeserializeObject<UserInfo>(responseString);
        //if (userInfo is null)
        //    _logger.LogError("Incorrect Json format in SignIn response");
        return userInfo;
    }
}
