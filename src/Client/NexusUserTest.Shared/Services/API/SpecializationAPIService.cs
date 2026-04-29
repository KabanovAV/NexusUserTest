using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ISpecializationAPIService
    {
        Task<ApiResponse<List<SpecializationDTO>>> GetAllSpecialization(string? include = null);
        Task<ApiResponse<List<SelectItem>>> GetSpecializationSelect();
        Task<ApiResponse<SpecializationDTO>> GetSpecialization(int id, string? include = null);
        Task<ApiResponse<SpecializationDTO>> AddSpecialization(SpecializationDTO item, string? include = null);
        Task<ApiResponse<Unit>> UpdateSpecialization(SpecializationDTO item);
        Task<ApiResponse<bool>> DeleteSpecialization(int id);
    }

    public class SpecializationAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : ISpecializationAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<SpecializationDTO>>> GetAllSpecialization(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/specializations", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<SpecializationDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllSpecialization");
            }
            return await _responseHandler.ExecuteHttpAsync<List<SpecializationDTO>>(() =>
                    _httpClient.GetAsync("api/specializations"), "GetAllSpecialization");
        }

        public async Task<ApiResponse<List<SelectItem>>> GetSpecializationSelect()
            => await _responseHandler.ExecuteHttpAsync<List<SelectItem>>(() =>
                _httpClient.GetAsync("api/specializations/select"), "GetAllSpecialization");

        public async Task<ApiResponse<SpecializationDTO>> GetSpecialization(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/specializations/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(() =>
                    _httpClient.GetAsync(url), "GetSpecialization");
            }
            return await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(() =>
                _httpClient.GetAsync($"api/specializations/{id}"), "GetSpecialization");
        }

        public async Task<ApiResponse<SpecializationDTO>> AddSpecialization(SpecializationDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/specializations", "include", include);
                return await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddSpecialization");
            }
            return await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(() =>
                _httpClient.PostAsJsonAsync("api/specializations", item), "AddSpecialization");
        }

        public async Task<ApiResponse<Unit>> UpdateSpecialization(SpecializationDTO item)
            => await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.PutAsJsonAsync($"api/specializations/{item.Id}", item), "UpdateSpecialization");


        public async Task<ApiResponse<bool>> DeleteSpecialization(int id)
            => await _responseHandler.ExecuteHttpAsync<bool>(() =>
                _httpClient.DeleteAsync($"api/specializations/{id}"), "DeleteSpecialization");
    }
}
