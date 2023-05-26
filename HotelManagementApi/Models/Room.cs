using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementApi.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public string? RoomType { get; set; }
        public int Price { get; set; }
        public bool Availability { get; set; }
        public Amenities? Amenitiess { get; set; }

        public Hotel? Hotel { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
