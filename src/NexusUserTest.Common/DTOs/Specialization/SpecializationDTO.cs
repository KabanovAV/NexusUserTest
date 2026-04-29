namespace NexusUserTest.Common
{
    public record SpecializationDTO(int Id, string Title);

    //public class SpecializationDTO
    //{
    //    public int Id { get; set; }
    //    [Display(Name = "Название")]
    //    [Required(ErrorMessage = "Обязательное поле для заполнения")]
    //    [StringLength(100, ErrorMessage = "Количество символов до 100")]
    //    public string Title { get; set; } = string.Empty;
    //}
}
