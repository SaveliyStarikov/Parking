using System.ComponentModel.DataAnnotations;

namespace Parking.Model
{
    public class Place
    {
        public int Id { get; set; } // Идентификатор парковочного места

        [Required]
        [Display(Name = "Номер места")]
        public string Name { get; set; } = string.Empty; // Название места (например, Место 1)

        [Required]
        [Display(Name = "Статус места")]
        public string Status { get; set; } = "Свободно"; // Статус: "Свободно" или "Занято"

        [Display(Name = "Информация об автомобиле")]
        public string? Car { get; set; } // Текст: номер машины + марка + модель

        [Display(Name = "Дата начала парковки")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Дата окончания парковки")]
        public DateTime? EndDate { get; set; }
    }
}
