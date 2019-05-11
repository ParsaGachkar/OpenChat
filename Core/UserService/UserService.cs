using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.UserService.Resources;
using Data.Domain;
using Data.Repositories.UserRepository;
using Data.UnitOfWork;
using System.Linq;

namespace Core.UserService
{
    public class UserService : IUserService
    {
        public async Task CreateUser(CreateUserResource model)
        {
            await (await unitOfWork.GetRepository<UserRepository, User, Guid>()).Create(mapper.Map<User>(model));
            await unitOfWork.Commit();
        }

        public async Task DeleteUser(DeleteUserResource model)
        {
            await (await unitOfWork.GetRepository<UserRepository, User, Guid>()).Delete(mapper.Map<User>(model));
            await unitOfWork.Commit();
        }

        public async Task EditUser(EditUserResource model)
        {
            await (await unitOfWork.GetRepository<UserRepository, User, Guid>()).Update(mapper.Map<User>(model));
            await unitOfWork.Commit();
        }

        public async Task<ICollection<ReadUserResource>> ReadAllUsers()
        {
            return (await (await unitOfWork.GetRepository<UserRepository, User, Guid>()).ReadAll()).Select(u => mapper.Map<ReadUserResource>(u)).ToArray();
        }

        public async Task<ReadUserResource> ReadUser(Guid Id)
        {
            return mapper.Map<ReadUserResource>(await (await unitOfWork.GetRepository<UserRepository, User, Guid>()).Read(Id));
        }
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;

        }
    }
}