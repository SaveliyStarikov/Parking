using Microsoft.AspNetCore.Mvc;
using Parking.Model;
using Parking.Data;

namespace Parking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public IActionResult GetCars()
        {
            var cars = _context.Cars.ToList();
            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST: api/Cars
        [HttpPost]
        public IActionResult CreateCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cars.Add(car);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] Car car)
        {
            if (id != car.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCar = _context.Cars.Find(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            existingCar.LicensePlate = car.LicensePlate;
            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
