using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;
using ProjectTracker.Repositories;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        

        public AuthController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        


        /* ================ Get all users ================== */

        [HttpGet, Authorize]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _userRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetUser(int id)
        {
            return await _userRepository.Get(id);
        }

        /* ================ Register New User ================== */

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] UserRegisterDTO model)
        {
            try
            {
                var newUser = await _userRepository.Create(model);
                return CreatedAtAction(nameof(GetUsers), new { username = newUser.Username }, newUser);
            }
            catch (SqliteException e)
            {
                return Ok("Username taken");
            }
        }

        /* ================ Login User ======= */

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUser([FromBody] UserLoginDTO model)
        {
            var user = await _userRepository.Get(model.UserName);
            if (user == null)
            {
                return NotFound();
            }

            string message = await _userRepository.Login(model);
            if (message.Equals("Password Verification Failed"))
            {
                return Ok(message);
            }
            return new LoginResponse
            {
                Id = user.Id, 
                Username = model.UserName, 
                Token = message
            };
            
            
        }




        /* ================ Edit User Profile======= */

        [HttpPut]
        public async Task<IActionResult> PutHotel(UserUpdateDTO user)
        {
            await _userRepository.Update(user);
            return NoContent();
        }

        /* ================ Delete User ======= */

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.Delete(id);
            return NoContent();
        }

    }
}
