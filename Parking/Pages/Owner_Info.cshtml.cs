using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Model; // ”бедитесь, что используетс€ правильное пространство имен

namespace Parking.Pages
{
    public class Owner_InfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Owner_InfoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Model.Owner> Owners { get; set; } = new List<Model.Owner>(); // явно указать Model.Owner
        public List<Model.Car> AvailableCars { get; set; } = new List<Model.Car>(); // явно указать Model.Car

        [BindProperty]
        public Model.Owner NewOwner { get; set; } // явно указать Model.Owner

        public async Task OnGetAsync()
        {
            Owners = await _context.Owners
                .Include(o => o.Car)
                .ToListAsync();

            AvailableCars = await _context.Cars.ToListAsync();
        }

        // ќстальные методы остаютс€ без изменений
        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.Owners.Add(NewOwner);
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
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}