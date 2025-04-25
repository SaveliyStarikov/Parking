using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Pages
{
    public class Avto_InfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Avto_InfoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Car> Cars { get; set; } = new List<Car>();

        [BindProperty]
        public Car CarInput { get; set; } = new Car();

        public async Task OnGetAsync()
        {
            Cars = await _context.Cars.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                Cars = await _context.Cars.ToListAsync();
                return Page();
            }

            _context.Cars.Add(CarInput);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                Cars = await _context.Cars.ToListAsync();
                return Page();
            }

            var car = await _context.Cars.FindAsync(CarInput.Id);
            if (car != null)
            {
                car.LicensePlate = CarInput.LicensePlate;
                car.Brand = CarInput.Brand;
                car.Model = CarInput.Model;

                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}