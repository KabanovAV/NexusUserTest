using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Application.Services;
using NexusUserTest.Common;
using SibCCSPETest.WebApi.MappingProfiles;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController(IRepoServiceManager service) : ControllerBase
    {
        private readonly IRepoServiceManager _service = service;

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SettingDTO>> Get(int id, string? include = null)
        {
            var setting = await _service.SettingRepository.GetSettingAsync(s => s.Id == id, include);
            if (setting == null)
                return NotFound(new { Message = $"Настройка с id {id} не найдена." });
            return Ok(setting.ToAdminDto());
        }

        [HttpPost]
        public async Task<ActionResult<SettingDTO>> Add(SettingCreateDTO settingCreateDTO, string? include = null)
        {
            if (settingCreateDTO == null)
                return BadRequest("Данные для добавления настройки пустые.");
            var setting = settingCreateDTO.ToEntity();
            await _service.SettingRepository.AddSettingAsync(setting!, include);
            var settingDTO = setting!.ToAdminDto();
            return CreatedAtAction(nameof(Get), new { id = settingDTO!.Id }, settingDTO);
        }

        [HttpPut]
        public async Task<ActionResult<SettingDTO>> Update(SettingDTO settingDTO, string? include = null)
        {
            if (settingDTO == null)
                return BadRequest("Данные для обновления настройки пустые.");
            var setting = await _service.SettingRepository.GetSettingAsync(s => s.Id == settingDTO.Id, include);
            if (setting == null)
                return NotFound(new { Message = $"Настройка с id {settingDTO.Id} не найдена." });
            setting.UpdateFromDto(settingDTO);
            await _service.SettingRepository.UpdateSettingAsync(setting, include);
            return Ok(setting.ToAdminDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var setting = await _service.SettingRepository.GetSettingAsync(s => s.Id == id);
            if (setting == null)
                return NotFound(new { Message = $"Настройка с id {id} не найдена." });
            await _service.SettingRepository.DeleteSettingAsync(setting);
            return NoContent();
        }
    }
}
