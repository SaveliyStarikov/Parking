using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Pages
{
    public class Owner_InfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Owner_InfoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Owner> Owners { get; set; } = new List<Owner>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public SelectList CarList { get; set; }

        [BindProperty]
        public Owner NewOwner { get; set; } = new Owner();

        public async Task OnGetAsync()
        {
            Owners = await _context.Owners.Include(o => o.Car).ToListAsync();
            Cars = await _context.Cars.ToListAsync();
            CarList = new SelectList(Cars, "Id", "LicensePlate");
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            // ”прощЄнна€ логика без сложных проверок
            var car = await _context.Cars.FindAsync(NewOwner.CarId);
            if (car == null) return Page();

            var owner = new Owner
            {
                Name = NewOwner.Name,
                Email = NewOwner.Email,
                Phone = NewOwner.Phone,
                CarId = NewOwner.CarId,
                Car = car
            };

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id, string name, string email, string phone, int carId)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                owner.Name = name;
                owner.Email = email;
                owner.Phone = phone;
                owner.CarId = carId;
                owner.Car = await _context.Cars.FindAsync(carId);

                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}