using System.ComponentModel.DataAnnotations;

namespace Parking.Model
{
    public class Place
    {
        public int Id { get; set; } // Идентификатор парковочного места

        [Required(ErrorMessage = "Необходимо указать автомобиль")]
        public required string Car { get; set; } = string.Empty; // Информация об автомобиле

        public DateTime StartDate { get; set; } = DateTime.Now; // Дата и время начала парковки
        public DateTime EndDate { get; set; } // Дата и время конца парковки
    }
}

