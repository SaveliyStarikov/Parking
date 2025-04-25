using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Model;
using System.Collections.Generic;
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

        [BindProperty]
        public Owner OwnerInput { get; set; } = new Owner();

        public List<Owner> Owners { get; set; } = new List<Owner>();
        public SelectList CarList { get; set; }

        public async Task OnGetAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            Owners = await _context.Owners
                .Include(o => o.Car)
                .ToListAsync();

            CarList = new SelectList(
                await _context.Cars.ToListAsync(),
                "Id",
                "LicensePlate");
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                return Page();
            }

            _context.Owners.Add(OwnerInput);
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

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                return Page();
            }

            var existingOwner = await _context.Owners.FindAsync(OwnerInput.Id);
            if (existingOwner != null)
            {
                existingOwner.Name = OwnerInput.Name;
                existingOwner.Email = OwnerInput.Email;
                existingOwner.Phone = OwnerInput.Phone;
                existingOwner.CarId = OwnerInput.CarId;

                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}