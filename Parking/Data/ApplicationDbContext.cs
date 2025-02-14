using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Parking.Model;

namespace Parking.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Конструктор, который принимает параметры конфигурации базы данных.
        // — Передаёт эти параметры в базовый класс DbContext с помощью : base(options).
        // — Это позволяет настраивать подключение к базе через Dependency Injection.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.Migrate(); // Автоматически применяет миграции и создаёт базу, если её нет
        } 

        public DbSet<Car> Car { get; set; } //таблица Car, содержащая данные о книгах.
        public DbSet<Parking_attendant> Parking_attendant { get; set; } //таблица Parking_attendant, содержащая данные о студентах.
        public DbSet<Parking_space> Parking_space { get; set; } //таблица Parking_space, хранящая информацию о выдачах книг студентам.
    }
}
