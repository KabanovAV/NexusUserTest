using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IResultAPIService
    {
        Task<List<ResultInfoAdminDTO>> GetAllInfoResultAsync(int groupUserId, string? include = null);
        Task<List<ResultTestDTO>> GetAllTestResultAsync(int groupUserId, string? include = null);
        Task<List<ResultInfoTestDTO>> GetAllTestInfoResultsync(int groupUserId, string? include = null);
        Task<ResultTestDTO?> GetTestResultAsync(int id, string? include = null);
        Task<ResultTestDTO?> AddTestResultAsync(ResultTestDTO entity, string? include = null);
        Task<List<ResultTestDTO>?> AddRangeTestResultAsync(IEnumerable<ResultTestDTO> entities, string? include = null);
        Task<ResultTestDTO?> UpdateTestResult(ResultTestDTO entity, string? include = null);
        Task DeleteInfoResult(int groupUserId);
    }

    public class ResultAPIService(IHttpClientFactory httpClienFactory) : IResultAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        /// <summary>
        /// Получение всех результатов теста пользователя группы
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает список результатов ResultInfoDTO</returns>
        public async Task<List<ResultInfoAdminDTO>> GetAllInfoResultAsync(int groupUserId, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/results/groupuser/{groupUserId}/info?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ResultInfoAdminDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// Получение всех результатов теста пользователя группы
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает список результатов ResultTestDTO</returns>
        public async Task<List<ResultTestDTO>> GetAllTestResultAsync(int groupUserId, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/results/groupuser/{groupUserId}/test?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ResultTestDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// Получение всех результатов теста пользователя группы
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает список результатов ResultTestDTO</returns>
        public async Task<List<ResultInfoTestDTO>> GetAllTestInfoResultsync(int groupUserId, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/results/info/groupuser/{groupUserId}/test?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ResultInfoTestDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// Получение результата теста пользователя группы
        /// </summary>
        /// <param name="id">Id результата</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает результат ResultTestDTO</returns>
        public async Task<ResultTestDTO?> GetTestResultAsync(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/results/{id}/test?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ResultTestDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Добавление результата в БД
        /// </summary>
        /// <param name="entity">Результат</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает результат ResultTestDTO</returns>
        public async Task<ResultTestDTO?> AddTestResultAsync(ResultTestDTO entity, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/results/test?include={include}", entity);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ResultTestDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Добавление результатов в БД
        /// </summary>
        /// <param name="entities">Список результатов</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает список результатов ResultTestDTO</returns>
        public async Task<List<ResultTestDTO>?> AddRangeTestResultAsync(IEnumerable<ResultTestDTO> entities, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/results/test/batch?include={include}", entities);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<ResultTestDTO>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// Обновление результата в БД
        /// </summary>
        /// <param name="entity">Результат</param>
        /// <param name="include">Подключение инклудов</param>
        /// <returns>Возвращает результат ResultTestDTO</returns>
        public async Task<ResultTestDTO?> UpdateTestResult(ResultTestDTO entity, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/results/test?include={include}", entity);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ResultTestDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Удаление результатов из БД
        /// </summary>
        /// <param name="groupUserId">Id группы пользователя</param>
        public async Task DeleteInfoResult(int groupUserId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/results/{groupUserId}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
