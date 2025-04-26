using System.ComponentModel.DataAnnotations;

namespace Parking.Model
{
    public class Owner
    {
        
        public int Id { get; set; }

        [Display(Name = "Имя владельца")]
        [Required(ErrorMessage = "Поле 'Имя владельца' обязательно для заполнения")]
        public string? Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string? Email { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Поле 'Телефон' обязательно для заполнения")]
        [RegularExpression(@"^\+?[0-9\s\-\(\)]+$",
            ErrorMessage = "Телефон должен содержать только цифры и символы ()-+")]
        public string? Phone { get; set; }

        [Display(Name = "Автомобиль")]
        [Required(ErrorMessage = "Необходимо указать автомобиль")]
        public int? CarId { get; set; }

        [Display(Name = "Автомобиль")]
        public Car? Car { get; set; }
    }
}