using HotelManagementApi.Models;
using HotelManagementApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IControllerMethods icontroller;
        public HotelController(IControllerMethods icontrollerr)
        {
            this.icontroller = icontrollerr;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok( icontroller.GetAllHotels());
        }
        [HttpPost, Authorize(Roles="admin")]
        [AllowAnonymous]
        public async Task<ActionResult> AddHotel(Hotel hotel)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("admin"))
            {
                return Unauthorized("You are Not authorized");
            }
            return Ok( icontroller.AddHotel(hotel)); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var hotel=icontroller.GetHotelById(id); 
            if(hotel!=null)
                return Ok( hotel);  
            return NotFound("Hotel Not found");
        }
        [HttpGet("{HotelName}")]
        public  async Task<ActionResult> Get(string name)
        {
            var hotel =  icontroller.GetHotelByName(name);
            if(hotel != null)
                return Ok(hotel);
            return NotFound("Hotel Not found");
        }
        [HttpPut, Authorize(Roles = "admin")]
        [AllowAnonymous]

        public async Task<ActionResult> UpdateHotell(Hotel hotel,int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("admin"))
            {
                return Unauthorized("You are Not authorized");
            }
            return Ok(icontroller.UpdateHotel(hotel,id));
        }
        [HttpDelete, Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("admin"))
            {
                return Unauthorized("You are Not authorized");
            }
            var hotel = icontroller.DeleteHotel(id);
            if (hotel != null)
                return Ok(hotel);
            return BadRequest("Hotel not found");
        }

      








    }
}
