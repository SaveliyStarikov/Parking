using System.ComponentModel.DataAnnotations;

namespace Parking.Model
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется номер автомобиля.")]
        [StringLength(15, ErrorMessage = "Номер автомобиля не может быть длиннее 15 символов.")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Требуется марка автомобиля.")]
        [StringLength(100, ErrorMessage = "Марка автомобиля не может быть длиннее 100 символов.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Требуется модель автомобиля.")]
        [StringLength(100, ErrorMessage = "Модель автомобиля не может быть длиннее 100 символов.")]
        public string Model { get; set; }
    }
}
