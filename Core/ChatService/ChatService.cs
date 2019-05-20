using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.ChatService.Resources;
using Core.UserService;
using Core.UserService.Resources;
using Data.Domain;
using Data.Repositories.ChatRepository;
using Data.Repositories.MessegeRepository;
using Data.UnitOfWork;

namespace Core.ChatService
{
    public class ChatService : IChatService
    {


        public async Task<ICollection<ChatReadResource>> GetChats(ReadChatResource model)
        {
            var chatRepository = await unitOfWork.GetRepository<ChatRepository, Chat, Guid>();
            return (await chatRepository.GetChatFor(model.currentUserId,model.selectedUserId)).UserChats.Select(uc=>uc.Chat).Select(c => mapper.Map<ChatReadResource>(c)).ToArray();
        }

        public async Task<ICollection<MessegeReadResource>> GetMesseges(ChatReadResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            return ((await messegeRepository.MessegesFor(model.Id)).Select(m => mapper.Map<MessegeReadResource>(m))).ToArray();
        }



        public async Task SendMessege(MessegeWriteResource model)
        {
            var chatRepository = await unitOfWork.GetRepository<ChatRepository, Chat, Guid>();
            var messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            Chat chat = await chatRepository.GetChatFor(model.SenderId, model.ReciverId);
            if (chat == null)
            {
                await chatRepository.CreateChatFor(model.SenderId, model.ReciverId);
            }
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

    public class ReadChatResource
    {
        public Guid selectedUserId{get;set;}
        public Guid currentUserId{get;set;}
    }
}