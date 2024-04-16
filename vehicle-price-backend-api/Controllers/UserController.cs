using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using vehicle_price_backend_api.Data;
using vehicle_price_backend_api.Models;

namespace vehicle_price_backend_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public static string Key = "ad##frw$";
        private readonly UserAPIDbCon dbContext;

        public UserController(UserAPIDbCon dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("UserRegistartion")]
        public async Task<IActionResult> UserRegistration(UserRegistrationDTO userRegistrationDTO)
        {
            try
            {
                var user = new User()
                {
                    Email = userRegistrationDTO.Email,
                    Name = userRegistrationDTO.Name,
                    Password = ConvertToEncrypt(userRegistrationDTO.Password),
                    PhoneNo = userRegistrationDTO.PhoneNo,
                    UserType = userRegistrationDTO.UserType
                };

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

                return Ok("User registered successfully.");
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == userLoginDTO.Email && u.Password == ConvertToEncrypt(userLoginDTO.Password));

                if (user == null)
                {
                    return NotFound();
                }

                return Ok("Login successful.");
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);

        }
    }
}
