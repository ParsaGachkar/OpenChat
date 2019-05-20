using System.Collections.ObjectModel;
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
using Data.Repositories.UserRepository;

namespace Core.ChatService
{
    public class ChatService : IChatService
    {


        public async Task<ICollection<ChatReadResource>> GetChats(ReadChatResource model)
        {
            var chatRepository = await unitOfWork.GetRepository<ChatRepository, Chat, Guid>();
            IEnumerable<Chat> chats = (await chatRepository.GetChats(model.currentUserId)) ?? new Collection<Chat>();
            return chats.Select(c => mapper.Map<ChatReadResource>(c)).ToList();
        }

        public async Task<ICollection<MessegeReadResource>> GetMesseges(ChatReadResource model)
        {
            MessegeRepository messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            ICollection<Messege> messeges = await messegeRepository.MessegesFor(model.Id) ?? new Collection<Messege>();
            return (messeges.Select(m => mapper.Map<MessegeReadResource>(m))).ToList();
        }



        public async Task SendMessege(MessegeWriteResource model)
        {
            var chatRepository = await unitOfWork.GetRepository<ChatRepository, Chat, Guid>();
            var messegeRepository = await unitOfWork.GetRepository<MessegeRepository, Messege, Guid>();
            var userRepository = await unitOfWork.GetRepository<UserRepository,User,Guid>();
            Chat chat = await chatRepository.GetChatFor(model.SenderId, model.ReciverId);
            if (chat == null)
            {
                chat = await chatRepository.CreateChatFor(model.SenderId, model.ReciverId);
            }
            var messege = mapper.Map<Messege>(model);
            messege.Id = Guid.NewGuid();
            messege.ReciverId = model.ReciverId;
            messege.SenderId = model.SenderId;
            messege.ChatId = chat.Id;
            messege.CreationDateTime = DateTime.Now;
            await messegeRepository.SendMessege(messege);
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