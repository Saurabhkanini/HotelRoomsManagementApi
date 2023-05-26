using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string ?HotelName { get; set; }
        public string ?Address { get; set; }
        public string ?City { get; set; }
        public string ?Country { get; set; }
        public int HotelCapacity { get; set; }
        public ICollection<Room> ?Rooms { get; set; }
    }
}
