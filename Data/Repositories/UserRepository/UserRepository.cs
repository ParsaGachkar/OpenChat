using System;
using Data.Domain;
using Data.Repositories.Abstracts;

namespace Data.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {

    }
}