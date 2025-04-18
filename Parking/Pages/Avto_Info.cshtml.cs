using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Parking.Data;
using Parking.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Pages
{
    public class Avto_InfoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Конструктор принимает контекст базы данных через DI (Dependency Injection)
        public Avto_InfoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Список автомобилей, загружаемый из базы данных
        public List<Car> Cars { get; set; } = new List<Car>();

        // Проперти для нового автомобиля
        [BindProperty]
        public Car NewCar { get; set; } = new Car();

        // Метод для обработки GET-запроса и загрузки данных
        public async Task OnGetAsync()
        {
            Cars = await _context.Cars.ToListAsync();
        }

        // Метод для добавления нового автомобиля в базу данных
        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!string.IsNullOrEmpty(NewCar.LicensePlate) && !string.IsNullOrEmpty(NewCar.Brand) && !string.IsNullOrEmpty(NewCar.Model))
            {
                _context.Cars.Add(NewCar);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(); // Перезагружаем страницу
        }

        // Метод для удаления автомобиля по Id
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(); // Перезагружаем страницу
        }

        // Метод для редактирования информации об автомобиле
        public async Task<IActionResult> OnPostEditAsync(int id, string licensePlate, string brand, string model)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                car.LicensePlate = licensePlate;
                car.Brand = brand;
                car.Model = model;

                await _context.SaveChangesAsync();
            }
            return RedirectToPage(); // Перезагружаем страницу
        }
    }
}
