using HotelManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly HotelDbContext uDbC;
        public AuthController(HotelDbContext udb, IConfiguration ic)
        {
            this.uDbC = udb;
            this.configuration = ic;

        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUser ud)
        {

            var existingUserCheck = uDbC.RegisterUsers.FirstOrDefault(x => x.Username == ud.Username);
            if (existingUserCheck == null)
            {
                string hashpassword = BCrypt.Net.BCrypt.HashPassword(ud.Password);


                var user = new RegisterUser()
                {
                    Username = ud.Username,
                    Password = hashpassword,
                    Email = ud.Email,
                    Role = ud.Role,

                };
                uDbC.Add(user);
                uDbC.SaveChanges();
                return Ok(user);

            }
            return BadRequest("UserName Already Exists");

        }
        [HttpPost("Login")]
        public async Task<ActionResult> SignIn(Login ud)
        {
            string uname = ud.UserName;
            string pass = ud.Password;
            var user = uDbC.RegisterUsers.FirstOrDefault(x => x.Username == uname);
            //user.Password == ud.Password
            if (user != null && BCrypt.Net.BCrypt.Verify(pass, user.Password))
            {
                var token = CreatToken(user);
                return Ok(new { token, user });
            }
            else
            {
                return BadRequest("user not found");
            }
        }

        private string CreatToken(RegisterUser ud)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ud.Username),
                new Claim(ClaimTypes.Role,ud.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
