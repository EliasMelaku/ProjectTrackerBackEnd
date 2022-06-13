using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;


namespace ProjectTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(ProjectContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        /* ======================== Get Methods =========================================== */
            
        public async Task<IEnumerable<UserModel>> Get()
        {
            var usersDto = new List<UserDTO>();
            var users = await _context.Users.Include(u => u.Projects).ToListAsync();
            foreach (UserModel user in users)
            {
                usersDto.Add(new UserDTO
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Profile = user.Profile,
                    Projects = user.Projects
                    
                    

                });
            }

            return users;
        }

        public async Task<UserModel> Get(int id)
        {
            return await _context.Users.Include(u => u.Projects).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserModel> Get(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        }


        /* ======================== Post Methods =========================================== */

        public async Task<UserDTO> Create(UserRegisterDTO user) 
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new UserModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Profile = user.Profile
            };

            try
            {
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception( e.InnerException.ToString().Substring(100, 10));
            }
            
            
                
            return new UserDTO
            {
                Username = newUser.Username, 
                FirstName = newUser.FirstName, 
                LastName = newUser.LastName, 
                Email = newUser.Email, 
                Profile = newUser.Profile
            };
            
            
        }

        public async Task<string> Login(UserLoginDTO user)
        {
            var myUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.UserName);
            var loginStatus = VerifyPassword(user.Password, myUser.PasswordHash, myUser.PasswordSalt);
            return (loginStatus) ? CreateToken(myUser) : "Password Verification Failed";

        }

        /* ======================== Put/Update Method =========================================== */

        public async Task Update(UserUpdateDTO user)
        {
            var updatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            updatedUser.Username = user.Username;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Email = user.Email;
            updatedUser.Profile = user.Profile;
            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
        }




        /* ======================== Delete Method =========================================== */

        public async Task Delete(int id)
        {

            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete != null)
            {
                _context.Remove(userToDelete);
            }

            await _context.SaveChangesAsync();

        }



        /* ======================== Helper Methods =========================================== */


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var enteredPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return enteredPasswordHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
