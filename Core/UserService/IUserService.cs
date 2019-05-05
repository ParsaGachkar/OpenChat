using System;
using System.Threading.Tasks;
using Core.UserService.Resources;

namespace Core.UserService
{
    public interface IUserService
    {
        Task CreateUser(CreateUserResource model);
        Task ReadUser(Guid Id);
        Task ReadAllUsers();
        Task EditUser(EditUserResource model);
        Task DeleteUser(DeleteUserResource model);

    }
}