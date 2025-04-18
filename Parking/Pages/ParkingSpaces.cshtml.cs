using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Parking.Model;
using Parking.Data; // для подключения контекста
using System.Linq;

namespace Parking.Pages
{
    public class ParkingSpacesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpacesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Car> Cars { get; set; } = new List<Car>();

        [BindProperty]
        public int? SelectedCarId { get; set; }

        [BindProperty]
        public Place NewPlace { get; set; } = new Place
        {
            Car = " ",
            StartDate = null,
            EndDate = null
        };

        public void OnGet()
        {
            Cars = _context.Cars.ToList();

            if (NewPlace.StartDate == null)
            {
                NewPlace.StartDate = DateTime.Now;
            }
        }

        public IActionResult OnPost()
        {
            Cars = _context.Cars.ToList();

            if (SelectedCarId == null || SelectedCarId == 0)
            {
                ModelState.AddModelError("SelectedCarId", "Пожалуйста, выберите автомобиль.");
            }

            if (NewPlace.StartDate == null)
            {
                ModelState.AddModelError("NewPlace.StartDate", "Пожалуйста, введите дату начала.");
            }

            if (NewPlace.EndDate == null)
            {
                ModelState.AddModelError("NewPlace.EndDate", "Пожалуйста, введите дату окончания.");
            }

            if (NewPlace.StartDate != null &&
                NewPlace.EndDate != null &&
                NewPlace.EndDate < NewPlace.StartDate)
            {
                ModelState.AddModelError("NewPlace.EndDate", "Дата окончания не может быть раньше даты начала.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var car = Cars.FirstOrDefault(c => c.Id == SelectedCarId);
            if (car != null)
            {
                NewPlace.Car = $"{car.LicensePlate} - {car.Brand} {car.Model}";
            }

            // Тут можно сохранить место в базу, если захочешь:
            // _context.Places.Add(NewPlace);
            // _context.SaveChanges();

            return RedirectToPage("ParkingSpaces");
        }
    }
}
