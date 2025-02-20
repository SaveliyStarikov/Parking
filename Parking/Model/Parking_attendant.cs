using static System.Reflection.Metadata.BlobBuilder;

namespace Parking.Model
{
    public class Parking_attendant
    {
        public int Id { get; set; } //Уникуальный индификатор поставленного автомобиля
        public int CarId { get; set; } //Индификатор авто
        public required Car Car { get; set; } //Позволяет получить информацию об автомобиле 
        public int Parking_spaseID { get; set; } //Индификатор парковочного места
        public required Parking_space Parking_Space { get; set; } //Навигационное свойство связанное с с моделью Parking_Space
        public DateTime LoanDate { get; set; } = DateTime.Now; // Текущие время записи 
        public DateTime? ReturnDate { get; set; }// Дата когда забрали автомобиль
    }
}
