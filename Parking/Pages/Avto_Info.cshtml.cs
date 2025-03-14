using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Pages
{
    public class Avto_InfoModel : PageModel
    {
        public List<Car> Cars { get; set; } = new List<Car>
        {
            new Car { Id = 1, LicensePlate = "À123ÂÑ 77", Brand = "Toyota", Model = "Camry" },
            new Car { Id = 2, LicensePlate = "Â456ÎÐ 99", Brand = "Mercedes", Model = "E-Class" },
            new Car { Id = 3, LicensePlate = "Ñ789ÊÕ 177", Brand = "Ferrari", Model = "488 GTB" },
            new Car { Id = 4, LicensePlate = "Å222ÌÐ 78", Brand = "BMW", Model = "X5" },
            new Car { Id = 5, LicensePlate = "Ò333ÒÒ 97", Brand = "Audi", Model = "A6" }
        };

        [BindProperty]
        public Car NewCar { get; set; } = new Car();

        public void OnGet() { }

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrEmpty(NewCar.LicensePlate) && !string.IsNullOrEmpty(NewCar.Brand) && !string.IsNullOrEmpty(NewCar.Model))
            {
                int newId = Cars.Max(c => c.Id) + 1;
                NewCar.Id = newId;
                Cars.Add(NewCar);
            }
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var car = Cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                Cars.Remove(car);
            }
            return Page();
        }

        public IActionResult OnPostEdit(int id, string licensePlate, string brand, string model)
        {
            var car = Cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                car.LicensePlate = licensePlate;
                car.Brand = brand;
                car.Model = model;
            }
            return Page();
        }
    }

    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}