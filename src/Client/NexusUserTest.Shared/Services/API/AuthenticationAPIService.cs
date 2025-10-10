using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NexusUserTest.Common;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IAuthenticationAPIService
    {
        Task<ApiResponse<JsonDocument>> LoginAsync(LoginDto user);
        Task LogoutAsync();
    }

    public class AuthenticationAPIService(IHttpClientFactory httpClienFactory, ILocalStorageService storage,
        AuthenticationStateProvider authProvider, IApiResponseHandler responseHandler)
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly ILocalStorageService _storage = storage;
        private readonly IApiResponseHandler _responseHandler = responseHandler;
        private readonly CustomAuthenticationStateProvider _authProvider = (CustomAuthenticationStateProvider)authProvider;

        public async Task<ApiResponse<JsonDocument>> LoginAsync(LoginDto user)
        {
            var response = await _responseHandler.ExecuteHttpAsync<JsonDocument>(() =>
                _httpClient.PostAsJsonAsync("api/auth/login", user), "LoginAsync");
            if (!response.Success) return response;

            var result = response.Data!.Deserialize<JsonElement>();
            var token = result.GetProperty("token").GetString()!;

            await _storage.SetItemAsync("authToken", token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            _authProvider.NotifyUserAuthentication(token);
            return response;
        }

        public async Task LogoutAsync()
        {
            await _storage.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _authProvider.NotifyUserLogout();
        }
    }
}
