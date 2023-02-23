using PrivateNote.Api.Contracts.Data;
using PrivateNote.Api.Contracts.Responses;
using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Tests.E2E.ApiLayer;

public class PrivateNoteClient
{
    private readonly string _baseUrl;
    public PrivateNoteClient()
    {
        _baseUrl = "http://192.168.1.104/api/Auth/";
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

    public async Task<string> SignInAsync(string userName, string password)
    {
        var signInData = new Dictionary<string, string>();
        signInData.Add("UserName", userName);
        signInData.Add("Password", password);
        HttpClient client = new();
        var json = JsonConvert.SerializeObject(signInData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_baseUrl + "SignIn", content);
        if (!response.IsSuccessStatusCode)
            //_logger.LogError("Incorrect Username or Password");
            return null;
        var responseString = await response.Content.ReadAsStringAsync();
        var signInResponse = JsonConvert.DeserializeObject<SignInResponse>(responseString);
        //if (signInResponse is null)
        //    _logger.LogError("Incorrect Json format in SignIn response");
        return signInResponse?.Token;
    }

    public async Task<bool> RsaSignUpAsync(string userName, string publicKey, string signature)
    {
        var signUpData = new Dictionary<string, string>();
        signUpData.Add("UserName", userName);
        signUpData.Add("PublicKey", publicKey);
        signUpData.Add("Signature", signature);
        HttpClient client = new();
        var json = JsonConvert.SerializeObject(signUpData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_baseUrl + "RsaSignUp", content);
        //if (!response.IsSuccessStatusCode)
        //    _logger.LogError("SignUp Not Success response : " + response.ToString());
        return response.IsSuccessStatusCode;
    }

    public async Task<string> RsaSignInAsync(string userName, string publicKey, string signature)
    {
        var signInData = new Dictionary<string, string>();
        signInData.Add("UserName", userName);
        signInData.Add("PublicKey", publicKey);
        signInData.Add("Signature", signature);
        HttpClient client = new();
        var json = JsonConvert.SerializeObject(signInData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_baseUrl + "RsaSignIn", content);
        if (!response.IsSuccessStatusCode)
            //_logger.LogError("Incorrect Signature or PublicKey");
            return null;
        string responseString = await response.Content.ReadAsStringAsync();
        var signInResponse = JsonConvert.DeserializeObject<RsaSignInResponse>(responseString);
        //if (signInResponse is null)
        //    _logger.LogError("Incorrect Json format in SignIn response");
        return signInResponse?.EncryptedToken;
    }

    public async Task<IUser?> WhoAmI(string token)
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        var response = await client.GetAsync(_baseUrl + "HowAmI");
        if (!response.IsSuccessStatusCode)
            //_logger.LogError("token is not valid");
            return null;
        var responseString = await response.Content.ReadAsStringAsync();
        var userDto = JsonConvert.DeserializeObject<UserDto>(responseString);
        //if (userInfo is null)
        //    _logger.LogError("Incorrect Json format in SignIn response");
        return userDto;
    }
}
