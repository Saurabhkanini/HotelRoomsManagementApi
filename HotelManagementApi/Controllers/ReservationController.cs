using HotelManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly HotelDbContext hotelDbContext;
        public ReservationController(HotelDbContext htdb)
        {
            this.hotelDbContext = htdb;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(hotelDbContext.Reservations.ToList());
        }
        [HttpPost,Authorize(Roles="user")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Reservation r)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("user"))
            {
                return Unauthorized("You are Not authorized");
            }
            else
            {
                var room = hotelDbContext.Rooms.Find(r.RoomId);

                if (room == null)
                {
                    return NotFound();
                }

                if (room.Availability)
                {
                    room.Availability = false;
                    hotelDbContext.Reservations.Add(r);
                    hotelDbContext.SaveChanges();

                    return Ok(r);
                }
                else
                {
                    return BadRequest("Room is already booked.");
                }
            }
        
           
        }
    }
}
