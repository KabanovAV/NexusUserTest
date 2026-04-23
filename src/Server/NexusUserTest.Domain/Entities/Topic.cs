using NexusUserTest.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Topic : AuditableEntityBase
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(100, ErrorMessage = "Количество до 100 символов")]
        public string Title { get; set; } = string.Empty;
        public int SpecializationId { get; set; }

        [JsonIgnore]
        public Specialization? Specialization { get; set; }
        [JsonIgnore]
        public List<TopicQuestion>? TopicQuestion { get; set; }
    }
}
