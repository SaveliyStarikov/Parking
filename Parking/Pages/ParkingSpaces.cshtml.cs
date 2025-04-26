using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Model;
using System.ComponentModel.DataAnnotations;

namespace Parking.Pages
{
    public class ParkingSpacesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpacesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Place> Places { get; set; } = new();
        public List<Car> Cars { get; set; } = new();
        public List<Owner> Owners { get; set; } = new(); // Новый список владельцев

        [BindProperty]
        [Required(ErrorMessage = "Пожалуйста, выберите автомобиль.")]
        public int? SelectedCarId { get; set; }

        [BindProperty]
        public int? SelectedPlaceId { get; set; }

        [BindProperty]
        [Display(Name = "Дата начала")]
        [Required(ErrorMessage = "Дата начала обязательна")]
        public DateTime? StartDate { get; set; }

        [BindProperty]
        [Display(Name = "Дата окончания")]
        [Required(ErrorMessage = "Дата окончания обязательна")]
        public DateTime? EndDate { get; set; }

        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnPostAddPlace()
        {
            var newPlace = new Place
            {
                Name = $"Место {_context.Places.Count() + 1}",
                Status = "Свободно"
            };

            _context.Places.Add(newPlace);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostOccupyPlace()
        {
            LoadData();

            if (SelectedPlaceId == null || SelectedCarId == null || StartDate == null || EndDate == null)
            {
                ModelState.AddModelError(string.Empty, "Пожалуйста, заполните все поля для занятия места.");
                return Page();
            }

            if (EndDate < StartDate)
            {
                ModelState.AddModelError(nameof(EndDate), "Дата окончания не может быть раньше даты начала.");
                return Page();
            }

            var place = _context.Places.FirstOrDefault(p => p.Id == SelectedPlaceId);
            var car = _context.Cars.FirstOrDefault(c => c.Id == SelectedCarId);

            if (place == null || car == null)
            {
                ModelState.AddModelError(string.Empty, "Не удалось найти выбранное место или автомобиль.");
                return Page();
            }

            place.Car = $"{car.LicensePlate} - {car.Brand} {car.Model}";
            place.StartDate = StartDate;
            place.EndDate = EndDate;
            place.Status = "Занято";

            TryValidateModel(place);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SaveChanges();
            return RedirectToPage();
        }

        public IActionResult OnPostFreePlace(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.Id == id);
            if (place != null)
            {
                place.Car = null;
                place.StartDate = null;
                place.EndDate = null;
                place.Status = "Свободно";
                _context.SaveChanges();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDeletePlace(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.Id == id);
            if (place != null)
            {
                _context.Places.Remove(place);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }

        private void LoadData()
        {
            Places = _context.Places.ToList();
            Cars = _context.Cars.ToList();
            Owners = _context.Owners.Include(o => o.Car).ToList(); // Подгружаем владельцев
        }

        public Owner? GetOwnerByCarInfo(string? carInfo)
        {
            if (string.IsNullOrEmpty(carInfo))
                return null;

            var licensePlate = carInfo.Split(" - ")[0]; // Извлекаем госномер
            return Owners.FirstOrDefault(o => o.Car != null && o.Car.LicensePlate == licensePlate);
        }
    }
}
