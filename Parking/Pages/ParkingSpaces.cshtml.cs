using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parking.Model;
using System.Collections.Generic;

namespace Parking.Pages
{
    public class ParkingSpacesModel : PageModel
    {
        [BindProperty]
        public Place NewPlace { get; set; } 

        public static List<Place> Places = new(); // Временное хранилище (замени на БД)

        public void OnGet()
        {
            // Можно загрузить данные из БД
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            NewPlace.Id = Places.Count + 1;
            Places.Add(NewPlace);

            return RedirectToPage("ParkingSpaces"); // Обновляем страницу
        }
    }
}
