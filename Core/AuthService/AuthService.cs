using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.AuthService.Config;
using Core.AuthService.Resources;
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

        private readonly AuthServiceConfig _authServiceConfig;

        public AuthService(IUserService userService, IMapper mapper, IOptions<AuthServiceConfig> authServiceConfig)
        {
            _authServiceConfig = authServiceConfig.Value;
            this.mapper = mapper;
            _userService = userService;
        }

        public async Task<UserGetResponseResource> GetUser(Resources.UserGetResource model)
        {
            return mapper.Map<UserGetResponseResource>(await userService.ReadUser(model.Id));
        }

        public async Task SendConfirmation(UserSendConfiramtionResource model)
        {
            phoneCodeDictionary.Add(model.PhoneNumber, random.Next(1000, 9999));
            await Task.CompletedTask;
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
    }
}