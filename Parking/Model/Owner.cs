using static System.Reflection.Metadata.BlobBuilder;

namespace Parking.Model
{
    public class Owner
    {
        public int Id { get; set; } //Индификатор владельца
        public required string Name { get; set; } //Имя владельца авто
        public string? Email { get; set; } //Электронная почта владельца авто
        public string? Phone { get; set; } //Телефон владельца авто
        public int CarId { get; set; } // Идентификатор машины, которую поставил владелец.
        public required Car Car { get; set; } //Позволяет получить информацию об автомобиле.

    }
}
