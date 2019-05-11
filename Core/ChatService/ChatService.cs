using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ChatService.Resources;
using Core.UserService;
using Core.UserService.Resources;
using Data.Domain;
using Data.Repositories.MessegeRepository;
using Data.UnitOfWork;

namespace Core.ChatService
{
    public class ChatService : IChatService
    {
        public async Task DeleteMessege(MessegeDeleteResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            await messegeRepository.Delete(messegeRepository.Read(model.Id));
        }

        public async Task EditMessege(MessegeUpdateResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            await messegeRepository.Update(mapper.Map<Messege>(model));
            await unitOfWork.Commit();
        }

        public async Task<ICollection<ChatReadResource>> GetChats(ReadUserResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();

            return (await messegeRepository.GetChatsFor(mapper.Map<User>(await userService.ReadUser(model.Id)))).Select(c => mapper.Map<ChatReadResource>(c)).ToArray();
        }

        public async Task<ICollection<MessegeReadResource>> GetMesseges(ChatReadResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            return ((await messegeRepository.MessegesFor(mapper.Map<Chat>(model))).Select(m => mapper.Map<MessegeReadResource>(m))).ToArray();
        }

        public async Task SeenChat(ChatReadResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            await messegeRepository.SeenChat(mapper.Map<Messege>(model));
            await unitOfWork.Commit();
        }

        public async Task SendMessege(MessegeWriteResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            await messegeRepository.SendMessege(mapper.Map<Messege>(model));
            await unitOfWork.Commit();
        }

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}