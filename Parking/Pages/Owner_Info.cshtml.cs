using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Pages
{
    public class Owner_InfoModel : PageModel
    {
        public List<Owner> Owners { get; set; } = new List<Owner>
        {
            new Owner { Id = 1, Name = "Иван Петров", Email = "ivan.petrov@example.com", Phone = "+7 900 123-45-67", CarId = 1, Car = new Car { LicensePlate = "А123ВС 77", Brand = "Toyota", Model = "Camry" } },
            new Owner { Id = 2, Name = "Алексей Смирнов", Email = "alexey.smirnov@example.com", Phone = "+7 921 456-78-90", CarId = 2, Car = new Car { LicensePlate = "В456ОР 99", Brand = "Mercedes", Model = "E-Class" } },
            new Owner { Id = 3, Name = "Мария Иванова", Email = "maria.ivanova@example.com", Phone = "+7 911 222-33-44", CarId = 3, Car = new Car { LicensePlate = "С789КХ 177", Brand = "Ferrari", Model = "488 GTB" } }
        };

        [BindProperty]
        public Owner NewOwner { get; set; } = new Owner { Car = new Car() };

        public void OnGet() { }

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrEmpty(NewOwner.Name) && NewOwner.Car != null)
            {
                int newId = Owners.Max(o => o.Id) + 1;
                NewOwner.Id = newId;
                Owners.Add(NewOwner);
            }
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var owner = Owners.FirstOrDefault(o => o.Id == id);
            if (owner != null)
            {
                Owners.Remove(owner);
            }
            return Page();
        }
    }

    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }

    public class Car
    {
        public string LicensePlate { get; set; } //kj
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}