using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.AuthService.Config;
using Core.AuthService.Resources;
using Core.SmsService;
using Core.UserService;
using Microsoft.Extensions.Options;

namespace Core.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserService.UserService userService;
        private Dictionary<string, int> phoneCodeDictionary = new Dictionary<string, int>();
        private Random random = new Random();

        private readonly IUserService _userService;
        private readonly IMapper mapper;
        private readonly ISmsService smsService;
        private readonly AuthServiceConfig _authServiceConfig;


        public async Task<UserGetResponseResource> GetUser(Resources.UserGetResource model)
        {
            return mapper.Map<UserGetResponseResource>(await userService.ReadUser(model.Id));
        }

        public async Task SendConfirmation(UserSendConfiramtionResource model)
        {
            int code = random.Next(1000, 9999);
            phoneCodeDictionary.Add(model.PhoneNumber, code);
            await smsService.Send(new SmsService.Resources.SmsResource(model.PhoneNumber, $"Your Verification Code is {code}"));
        }

        public async Task<UserVerifyResponseResource> VerifyConfirmation(UserVerifyResource model)
        {
            if (phoneCodeDictionary.ContainsKey(model.PhoneNumber))
            {
                if (phoneCodeDictionary[model.PhoneNumber] == model.Code)
                {
                    phoneCodeDictionary.Remove(model.PhoneNumber);
                    return new UserVerifyResponseResource(model, this._authServiceConfig);
                }
            }
            await Task.CompletedTask;
            throw new Exceptions.LoginFaildExeption();
        }
        public AuthService(IUserService userService, IMapper mapper, IOptions<AuthServiceConfig> authServiceConfig, ISmsService smsService)
        {
            _authServiceConfig = authServiceConfig.Value;
            this.mapper = mapper;
            this.smsService = smsService;
            _userService = userService;
        }
    }
}