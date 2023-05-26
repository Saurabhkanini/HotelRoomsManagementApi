using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.Models
{
    public class Amenities
    {
        [Key]
        public int Id { get; set; }
        public bool Tv { get; set; }
        public bool Ac { get; set; }
        public bool Snacks { get; set; }
        public bool Drinks { get; set; }

    }
}
