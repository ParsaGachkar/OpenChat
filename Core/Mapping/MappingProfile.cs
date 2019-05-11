using AutoMapper;
using Core.AuthService.Resources;
using Core.ChatService.Resources;
using Core.UserService.Resources;
using Data.Domain;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserGetResource, User>().ReverseMap();
            CreateMap<UserGetResponseResource, User>().ReverseMap();
            CreateMap<UserSendConfiramtionResource, User>().ReverseMap();
            CreateMap<UserVerifyResource, User>().ReverseMap();
            CreateMap<UserVerifyResponseResource, User>().ReverseMap();

            CreateMap<ChatReadResource, Chat>().ReverseMap();

            CreateMap<MessegeDeleteResource, Messege>().ReverseMap();
            CreateMap<MessegeReadResource, Messege>().ReverseMap();
            CreateMap<MessegeUpdateResource, Messege>().ReverseMap();
            CreateMap<MessegeWriteResource, Messege>().ReverseMap();

            CreateMap<CreateUserResource, User>().ReverseMap();
            CreateMap<DeleteUserResource, User>().ReverseMap();
            CreateMap<EditUserResource, User>().ReverseMap();
            CreateMap<ReadUserResource, User>().ReverseMap();

        }
    }
}