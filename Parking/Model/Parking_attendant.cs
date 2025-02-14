using static System.Reflection.Metadata.BlobBuilder;

namespace Parking.Model
{
    public class Parking_attendant
    {
        public int Id { get; set; } 
        public int CarId { get; set; } 
        public required Car Car { get; set; } 
        public int Parking_spaseID { get; set; } 
        public required Parking_space Parking_Space { get; set; } 
        public DateTime LoanDate { get; set; } = DateTime.Now; 
        public DateTime? ReturnDate { get; set; } 
    }
}
