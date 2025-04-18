using System.ComponentModel.DataAnnotations;

namespace Parking.Model
{
    public class Place
    {
        public int Id { get; set; } // Идентификатор парковочного места

        [Required(ErrorMessage = "Необходимо указать автомобиль")]
        public required string Car { get; set; } = string.Empty; // Информация об автомобиле
        [Required(ErrorMessage = "Пожалуйста, введите дату начала.")]
        public DateTime? StartDate { get; set; } = DateTime.Now; // Дата и время начала парковки
        [Required(ErrorMessage = "Пожалуйста, введите дату окончания.")]
        public DateTime? EndDate { get; set; } // Дата и время конца парковки
    }
}

