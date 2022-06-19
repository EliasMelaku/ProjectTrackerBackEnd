using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<SimpleUser>> Get();

        Task<UserModel> Get(int id);

        Task<UserModel> Get(string username);

        Task<UserDTO> Create(UserRegisterDTO user);

        Task<string> Login(UserLoginDTO user);

        Task Update(UserUpdateDTO user);

        Task<string> Update(ChangePassword password);

        Task Delete(int id);
    }
}
