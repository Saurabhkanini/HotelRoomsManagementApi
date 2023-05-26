using HotelManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly HotelDbContext hotelDbContext;
        public FiltersController(HotelDbContext htdb)
        {
            this.hotelDbContext = htdb;
        }
        [HttpGet("rooms/filter"),Authorize(Roles="user")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoomsByAmenities(bool tv = false, bool ac = false, bool snacks = false, bool drinks = false)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("user"))
            {
                return Unauthorized("You are Not authorized");
            }
            var query = hotelDbContext.Rooms.AsQueryable();

            // Apply filters based on amenity availability
            query = query.Where(r =>
                (tv && r.Amenitiess.Tv) ||
                (ac && r.Amenitiess.Ac) ||
                (snacks && r.Amenitiess.Snacks) ||
                (drinks && r.Amenitiess.Drinks)
            );

            var filteredRooms = await query.Include(r => r.Amenitiess).ToListAsync();

            return Ok(filteredRooms);
        }
        [HttpGet("hotelsLocation/filter"),Authorize(Roles ="user")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHotelsByLocationAndPrice(string location)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("user"))
            {
                return Unauthorized("You are Not authorized");
            }
            var query = hotelDbContext.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(h => h.City.ToLower() == location.ToLower());
            }

            var filteredHotels = await query.Include(h => h.Rooms).ToListAsync();

            return Ok(filteredHotels);
        }
        [HttpGet("roomPrice/filter"),Authorize(Roles ="user")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHotelsByPrice(int? maxPrice)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("user"))
            {
                return Unauthorized("You are Not authorized");
            }
            var query = hotelDbContext.Hotels.AsQueryable();

            // Apply filter based on maximum room price
            if (maxPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.Price <= maxPrice.Value));
            }

            var filteredHotels = await query.Include(h => h.Rooms).ToListAsync();

            // Exclude rooms with prices greater than the specified maximum price
            if (maxPrice.HasValue)
            {
                foreach (var hotel in filteredHotels)
                {
                    hotel.Rooms = hotel.Rooms.Where(r => r.Price <= maxPrice.Value).ToList();
                }
            }

            return Ok(filteredHotels);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoomCountByHotel(int id)
        {
            var hotel = await hotelDbContext.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            var roomCount = hotel.Rooms.Count;

            return Ok($"Totoal Number of RoomBooked with Hotel id {id} with id {roomCount}");
        }
    }
}
