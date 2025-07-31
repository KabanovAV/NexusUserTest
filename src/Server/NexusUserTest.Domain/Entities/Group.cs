using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NexusUserTest.Domain.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [StringLength(200, ErrorMessage = "Количество до 200 символов")]
        public string Title { get; set; } = string.Empty;
        public int SpecializationId { get; set; }
        [Display(Name = "Начало курсов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public DateTime Begin { get; set; }
        [Display(Name = "Окончание курсов")]
        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        public DateTime End { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }

        [JsonIgnore]
        public Specialization? Specialization { get; set; }
        [JsonIgnore]
        public List<GroupUser>? GroupUser { get; set; }
        [JsonIgnore]
        public Setting? Setting { get; set; }
    }
}
