using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ISpecializationAPIService
    {
        Task<ApiResponse<List<SpecializationDTO>>> GetAllSpecialization(string? include = null);
        Task<ApiResponse<List<SelectItem>>> GetSpecializationSelect();
        Task<ApiResponse<SpecializationDTO>> GetSpecialization(int id, string? include = null);
        Task<ApiResponse<SpecializationDTO>> AddSpecialization(SpecializationDTO item, string? include = null);
        Task<ApiResponse<SpecializationDTO>> UpdateSpecialization(SpecializationDTO item, string? include = null);
        Task<ApiResponse<bool>> DeleteSpecialization(int id);
    }

    public class SpecializationAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : ISpecializationAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<SpecializationDTO>>> GetAllSpecialization(string? include = null)
            => await _responseHandler.ExecuteHttpAsync<List<SpecializationDTO>>(async () =>
            {
                return await _httpClient.GetAsync($"api/specializations?include={include}");
            }, "GetAllSpecialization");

        public async Task<ApiResponse<List<SelectItem>>> GetSpecializationSelect()
            => await _responseHandler.ExecuteHttpAsync<List<SelectItem>>(async () =>
            {
                return await _httpClient.GetAsync($"api/specializations/select");
            }, "GetAllSpecialization");

        public async Task<ApiResponse<SpecializationDTO>> GetSpecialization(int id, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(async () =>
            {
                return await _httpClient.GetAsync($"api/specializations/{id}?include={include}");
            }, "GetSpecialization");

        public async Task<ApiResponse<SpecializationDTO>> AddSpecialization(SpecializationDTO item, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(async () =>
            {
                return await _httpClient.PostAsJsonAsync($"api/specializations?include={include}", item);
            }, "AddSpecialization");

        public async Task<ApiResponse<SpecializationDTO>> UpdateSpecialization(SpecializationDTO item, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<SpecializationDTO>(async () =>
            {
                return await _httpClient.PutAsJsonAsync($"api/specializations/{item.Id}?include={include}", item);
            }, "UpdateSpecialization");

        public async Task<ApiResponse<bool>> DeleteSpecialization(int id)
            => await _responseHandler.ExecuteAsync<bool>(async () =>
            {
                var response = await _httpClient.DeleteAsync($"api/specializations/{id}");
                return await response.Content.ReadFromJsonAsync<bool>();
            }, "DeleteSpecialization");
    }
}
