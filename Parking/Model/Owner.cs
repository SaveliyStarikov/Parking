using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace Parking.Model
{
    public class Owner
    {
        public Owner()
        {
            Name = string.Empty;
            Car = new Car();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Имя владельца обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [RegularExpression(@"^\+?[0-9\s\-\(\)]+$", ErrorMessage = "Некорректный формат телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Автомобиль обязателен")]
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
