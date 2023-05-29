using HotelManagementApi.Models;
using HotelManagementApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IControllerMethods icontroller;
        public RoomsController(IControllerMethods icontrollerr)
        {
            this.icontroller = icontrollerr;
        }
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms=icontroller.GetAllRooms();
            if(rooms!=null)
                return Ok(rooms);   
            return NotFound("Rooms Not available");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRoom(int id)
        {
            var room=icontroller.GetRoomById(id);
            if(room!=null)
                return Ok(room);
            return BadRequest("Room Not found");
        }
        [HttpPost,Authorize(Roles="manager")]
        [AllowAnonymous]
        public async Task<ActionResult> AddRoom(Room r)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("manager"))
            {
                return Unauthorized("You are Not authorized");
            }
            return Ok(icontroller.AddRoom(r));

        }
        [HttpPut("{id}"),Authorize(Roles ="manager")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateRoom(Room room,int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("manager"))
            {
                return Unauthorized("You are Not authorized");
            }
            return Ok(icontroller.UpdateRoom(room,id));
        }
        [HttpDelete("{id}"),Authorize(Roles ="manager")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("manager"))
            {
                return Unauthorized("You are Not authorized");
            }
            var room=icontroller.DeleteRoom(id);
            if(room!=null)
                return Ok(room);
            return NotFound("Room Not found");
        }


    }
}
