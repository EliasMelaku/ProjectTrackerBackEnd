using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> Get();

        Task<UserModel> Get(int id);

        Task<UserModel> Get(string username);

        Task<UserDTO> Create(UserRegisterDTO user);

        Task<string> Login(UserLoginDTO user);

        Task Update(UserUpdateDTO user);

        Task Delete(int id);
    }
}
