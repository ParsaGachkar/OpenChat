using System.Collections;
using System;
using System.Threading.Tasks;
using Core.UserService.Resources;
using System.Collections.Generic;

namespace Core.UserService
{
    public interface IUserService
    {
        Task CreateUser(CreateUserResource model);
        Task<ReadUserResource> ReadUser(Guid Id);
        Task<ICollection<ReadUserResource>> ReadAllUsers();
        Task EditUser(EditUserResource model);
        Task DeleteUser(DeleteUserResource model);

    }
}