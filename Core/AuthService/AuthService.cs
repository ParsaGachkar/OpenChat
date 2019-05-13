using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.AuthService.Config;
using Core.AuthService.Resources;
using Core.SmsService;
using Core.UserService;
using Data.Domain;
using Data.Repositories.UserRepository;
using Data.UnitOfWork;
using Microsoft.Extensions.Options;

namespace Core.AuthService
{
    public class AuthService : IAuthService
    {
        private static Dictionary<string, int> phoneCodeDictionary = new Dictionary<string, int>();
        private Random random = new Random();

        private readonly IUserService _userService;
        private readonly IMapper mapper;
        private readonly ISmsService smsService;
        private readonly IUnitOfWork unitOfWork;
        private readonly AuthServiceConfig _authServiceConfig;


        public async Task<UserGetResponseResource> GetUser(Resources.UserGetResource model)
        {
            return mapper.Map<UserGetResponseResource>(await _userService.ReadUser(model.Id));
        }

        public async Task SendConfirmation(UserSendConfiramtionResource model)
        {
            int code = random.Next(1000, 9999);
            if (!phoneCodeDictionary.ContainsKey(model.PhoneNumber))
                phoneCodeDictionary.Add(model.PhoneNumber, code);
            else
            {
                phoneCodeDictionary[model.PhoneNumber] = code;
            }
            await smsService.Send(new SmsService.Resources.SmsResource(model.PhoneNumber, $"Your Verification Code is {code}"));
        }

        public async Task<UserVerifyResponseResource> VerifyConfirmation(UserVerifyResource model)
        {
            if (phoneCodeDictionary.ContainsKey(model.PhoneNumber))
            {
                if (phoneCodeDictionary[model.PhoneNumber] == model.Code)
                {
                    await (await unitOfWork.GetRepository<UserRepository, User, Guid>()).Create(new User()
                    {
                        CreationDateTime = DateTime.Now,
                        DeleterId = null,
                        PhoneNumber = model.PhoneNumber
                    });
                    await unitOfWork.Commit();
                    phoneCodeDictionary.Remove(model.PhoneNumber);
                    return new UserVerifyResponseResource(model, this._authServiceConfig);
                }
            }
            await Task.CompletedTask;
            throw new Exceptions.LoginFaildExeption();
        }
        public AuthService(IUserService userService, IMapper mapper, IOptions<AuthServiceConfig> authServiceConfig, ISmsService smsService, IUnitOfWork unitOfWork)
        {
            _authServiceConfig = authServiceConfig.Value;
            this.mapper = mapper;
            this.smsService = smsService;
            this.unitOfWork = unitOfWork;
            _userService = userService;
        }
    }
}