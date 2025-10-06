using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IResultAPIService
    {
        Task<ApiResponse<List<ResultInfoAdminDTO>>> GetAllInfoResultAsync(int groupUserId, string? include = null);
        Task<ApiResponse<List<ResultInfoTestDTO>>> GetAllTestInfoResultAsync(int groupUserId, string? include = null);
        Task<ApiResponse<List<ResultTestDTO>>> GetAllTestResultAsync(int groupUserId, string? include = null);
        Task<ApiResponse<ResultTestDTO>> GetTestResultAsync(int id, string? include = null);
        Task<ApiResponse<ResultTestDTO>> AddTestResultAsync(ResultTestDTO entity, string? include = null);
        Task<ApiResponse<List<ResultTestDTO>>> AddRangeTestResultAsync(IEnumerable<ResultTestDTO> entities, string? include = null);
        Task<ApiResponse<Unit>> UpdateTestResult(ResultTestDTO entity);
        Task<ApiResponse<Unit>> DeleteInfoResult(int groupUserId);
    }

    public class ResultAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IResultAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        /// <summary>
        /// Получение всех результатов теста пользователя группы
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает ответ состояния запроса со списком результатов ResultInfoDTO</returns>
        public async Task<ApiResponse<List<ResultInfoAdminDTO>>> GetAllInfoResultAsync(int groupUserId, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/results/groupuser/{groupUserId}/info", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<ResultInfoAdminDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllInfoResultAsync");
            }
            return await _responseHandler.ExecuteHttpAsync<List<ResultInfoAdminDTO>>(() =>
                    _httpClient.GetAsync($"api/results/groupuser/{groupUserId}/info"), "GetAllInfoResultAsync");
        }

        /// <summary>
        /// Получение всех результатов теста пользователя группы
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает ответ состояния запроса со списком результатов ResultTestDTO</returns>
        public async Task<ApiResponse<List<ResultInfoTestDTO>>> GetAllTestInfoResultAsync(int groupUserId, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/results/groupuser/{groupUserId}/test-info", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<ResultInfoTestDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllTestInfoResultAsync");
            }
            return await _responseHandler.ExecuteHttpAsync<List<ResultInfoTestDTO>>(() =>
                    _httpClient.GetAsync($"api/results/groupuser/{groupUserId}/test-info"), "GetAllTestInfoResultAsync");
        }

        /// <summary>
        /// Получение всех результатов теста пользователя группы
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает ответ состояния запроса со списком результатов ResultTestDTO</returns>
        public async Task<ApiResponse<List<ResultTestDTO>>> GetAllTestResultAsync(int groupUserId, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/results/groupuser/{groupUserId}/test", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<ResultTestDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllTestResultAsync");
            }
            return await _responseHandler.ExecuteHttpAsync<List<ResultTestDTO>>(() =>
                    _httpClient.GetAsync($"api/results/groupuser/{groupUserId}/test"), "GetAllTestResultAsync");
        }

        /// <summary>
        /// Получение результата теста пользователя группы
        /// </summary>
        /// <param name="id">Id результата</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает ответ состояния запроса с результатом ResultTestDTO</returns>
        public async Task<ApiResponse<ResultTestDTO>> GetTestResultAsync(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/results/{id}/test", "include", include);
                return await _responseHandler.ExecuteHttpAsync<ResultTestDTO>(() =>
                    _httpClient.GetAsync(url), "GetTestResultAsync");
            }
            return await _responseHandler.ExecuteHttpAsync<ResultTestDTO>(() =>
                    _httpClient.GetAsync($"api/results/{id}/test"), "GetTestResultAsync");
        }

        /// <summary>
        /// Добавление результата в БД
        /// </summary>
        /// <param name="entity">Результат</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает ответ состояния запроса с результатом ResultTestDTO</returns>
        public async Task<ApiResponse<ResultTestDTO>> AddTestResultAsync(ResultTestDTO entity, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/results/test", "include", include);
                return await _responseHandler.ExecuteHttpAsync<ResultTestDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, entity), "AddTestResultAsync");
            }
            return await _responseHandler.ExecuteHttpAsync<ResultTestDTO>(() =>
                    _httpClient.PostAsJsonAsync("api/results/test", entity), "AddTestResultAsync");
        }

        /// <summary>
        /// Добавление результатов в БД
        /// </summary>
        /// <param name="entities">Список результатов</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает ответ состояния запроса со списком результатов ResultTestDTO</returns>
        public async Task<ApiResponse<List<ResultTestDTO>>> AddRangeTestResultAsync(IEnumerable<ResultTestDTO> entities, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/results/test/batch", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<ResultTestDTO>>(() =>
                    _httpClient.PostAsJsonAsync(url, entities), "AddRangeTestResultAsync");
            }
            return await _responseHandler.ExecuteHttpAsync<List<ResultTestDTO>>(() =>
                    _httpClient.PostAsJsonAsync("api/results/test/batch", entities), "AddRangeTestResultAsync");
        }

        /// <summary>
        /// Обновление результата в БД
        /// </summary>
        /// <param name="entity">Результат</param>
        /// <returns>Возвращает ответ состояния запроса</returns>
        public async Task<ApiResponse<Unit>> UpdateTestResult(ResultTestDTO entity)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PatchAsJsonAsync($"api/results/{entity.Id}/test", entity), "UpdateTestResult");

        /// <summary>
        /// Удаление результатов из БД
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// /// <returns>Возвращает ответ состояния запроса</returns>
        public async Task<ApiResponse<Unit>> DeleteInfoResult(int groupUserId)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.DeleteAsync($"api/results/{groupUserId}"), "DeleteInfoResult");
    }
}
