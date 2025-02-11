using System.ComponentModel.DataAnnotations;

namespace Parking.Model
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется номер автомобиля.")]
        [StringLength(6, ErrorMessage = "Заголовок не может быть длиннее 6 символов.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Требуется марка автомобиля.")]
        [StringLength(100, ErrorMessage = "Марка автомобиля не может быть длиннее 100 символов.")]
        public required string Author { get; set; }

    }
}
