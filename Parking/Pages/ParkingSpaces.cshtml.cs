using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Model;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Pages
{
    public class ParkingSpacesModel : PageModel
    {
        public List<Car> Cars { get; set; } = new List<Car>();

        [BindProperty]
        public int SelectedCarId { get; set; }  // ID выбранного авто

        [BindProperty]
        public Place NewPlace { get; set; } = new Place { Car = " " };

        public void OnGet()
        {
           
            Cars = new Avto_InfoModel().Cars;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            
            var car = Cars.FirstOrDefault(c => c.Id == SelectedCarId);
            if (car != null)
            {
                NewPlace.Car = $"{car.LicensePlate} - {car.Brand} {car.Model}"; 
            }

            return RedirectToPage("ParkingSpaces");
        }
    }
}