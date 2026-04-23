using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Specialization : AuditableEntityBase
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(100, ErrorMessage = "Количество до 100 символов")]
        public string Title { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Group>? Groups { get; set; }
        [JsonIgnore]
        public List<Topic>? Topics { get; set; }
    }
}
