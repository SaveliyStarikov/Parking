using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Parking.Model;
using Parking.Model.AuthApp;

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

        public DbSet<Car> Cars { get; set; } //таблица Cars, содержащая данные о книгах.
        public DbSet<Owner> Owners { get; set; } //таблица Parking_attendant, содержащая данные о студентах.
        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }  //таблица Parking_space, хранящая информацию о выдачах книг студентам.
    }
}
