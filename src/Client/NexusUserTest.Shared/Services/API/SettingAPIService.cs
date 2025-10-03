using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ISettingAPIService
    {
        Task<ApiResponse<SettingDTO>> GetSetting(int id, string? include = null);
        Task<ApiResponse<SettingDTO>> AddSetting(SettingDTO item, string? include = null);
        Task<ApiResponse<Unit>> UpdateSetting(SettingDTO item);
        Task<ApiResponse<Unit>> DeleteSetting(int id);
    }

    public class SettingAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : ISettingAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<SettingDTO>> GetSetting(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/settings/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<SettingDTO>(() =>
                    _httpClient.GetAsync(url), "GetSetting");
            }
            return await _responseHandler.ExecuteHttpAsync<SettingDTO>(() =>
                    _httpClient.GetAsync($"api/settings/{id}"), "GetSetting");
        }

        public async Task<ApiResponse<SettingDTO>> AddSetting(SettingDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/settings", "include", include);
                return await _responseHandler.ExecuteHttpAsync<SettingDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddSetting");
            }
            return await _responseHandler.ExecuteHttpAsync<SettingDTO>(() =>
                    _httpClient.PostAsJsonAsync("api/settings", item), "AddSetting");
        }

        public async Task<ApiResponse<Unit>> UpdateSetting(SettingDTO item)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PutAsJsonAsync($"api/settings/{item.Id}", item), "UpdateSetting");

        public async Task<ApiResponse<Unit>> DeleteSetting(int id)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.DeleteAsync($"api/settings/{id}"), "DeleteSetting");
    }
}
