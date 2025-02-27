namespace Parking.Model
{
    public class Place
    {
        public int Id { get; set; } //Индификатор парковочного места
        public required Car Car { get; set; } //Позволяет получить информацию об автомобиле 
        public DateTime StartDate { get; set; } = DateTime.Now; //Дата и время начала парковки
        public DateTime EndDate { get; set; }// Дата и время конца парковки
    }
}
