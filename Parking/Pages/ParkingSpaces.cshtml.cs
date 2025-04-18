using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Parking.Model;
using System.Linq;

namespace Parking.Pages
{
    public class ParkingSpacesModel : PageModel
    {
        public List<Car> Cars { get; set; } = new List<Car>();

        [BindProperty]
        public int? SelectedCarId { get; set; }

        [BindProperty]
        public Place NewPlace { get; set; } = new Place
        {
            Car = " ",
            StartDate = null,  // Начальная дата изначально null
            EndDate = null     // Конечная дата изначально null
        };

        public void OnGet()
        {
            Cars = new Avto_InfoModel().Cars;

            // Устанавливаем текущую дату для StartDate, если она еще не задана
            if (NewPlace.StartDate == null)
            {
                NewPlace.StartDate = DateTime.Now;
            }
        }

        public IActionResult OnPost()
        {
            Cars = new Avto_InfoModel().Cars;

            // Проверка: не выбрана машина
            if (SelectedCarId == null || SelectedCarId == 0)
            {
                ModelState.AddModelError("SelectedCarId", "Пожалуйста, выберите автомобиль.");
            }

            // Проверка: пустая дата начала
            if (NewPlace.StartDate == null)
            {
                ModelState.AddModelError("NewPlace.StartDate", "Пожалуйста, введите дату начала.");
            }

            // Проверка: пустая дата окончания
            if (NewPlace.EndDate == null)
            {
                ModelState.AddModelError("NewPlace.EndDate", "Пожалуйста, введите дату окончания.");
            }

            // Проверка: конец раньше начала
            if (NewPlace.StartDate != null &&
                NewPlace.EndDate != null &&
                NewPlace.EndDate < NewPlace.StartDate)
            {
                ModelState.AddModelError("NewPlace.EndDate", "Дата окончания не может быть раньше даты начала.");
            }

            // Если есть ошибки, возвращаем страницу с ошибками
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var car = Cars.FirstOrDefault(c => c.Id == SelectedCarId);
            if (car != null)
            {
                NewPlace.Car = $"{car.LicensePlate} - {car.Brand} {car.Model}";
            }

            // логика сохранения

            return RedirectToPage("ParkingSpaces");
        }
    }
}
