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

namespace Core.AuthService {
    public class AuthService : IAuthService {
        private static Dictionary<string, int> phoneCodeDictionary = new Dictionary<string, int> ();
        private Random random = new Random ();

        private readonly IUserService _userService;
        private readonly IMapper mapper;
        private readonly ISmsService smsService;
        private readonly IUnitOfWork unitOfWork;

        public async Task<UserGetResponseResource> GetUser (Resources.UserGetResource model) {
            return mapper.Map<UserGetResponseResource> (await _userService.ReadUser (model.Id));
        }

        public async Task SendConfirmation (UserSendConfiramtionResource model) {
            int code = random.Next (1000, 9999);
            if (!phoneCodeDictionary.ContainsKey (model.PhoneNumber))
                phoneCodeDictionary.Add (model.PhoneNumber, code);
            else {
                phoneCodeDictionary[model.PhoneNumber] = code;
            }
            await smsService.Send (new SmsService.Resources.SmsResource (model.PhoneNumber, $"Your Verification Code is {code}"));
        }

        public async Task<UserVerifyResponseResource> VerifyConfirmation (UserVerifyResource model) {
            if (phoneCodeDictionary.ContainsKey (model.PhoneNumber)) {
                if (phoneCodeDictionary[model.PhoneNumber] == model.Code) {
                    var user = await (await unitOfWork.GetRepository<UserRepository, User, Guid> ()).ReadByPhone (model.PhoneNumber);
                    if (user == null) {
                        await (await unitOfWork.GetRepository<UserRepository, User, Guid> ()).Create (new User () {
                            CreationDateTime = DateTime.Now,
                                PhoneNumber = model.PhoneNumber
                        });
                        await unitOfWork.Commit ();
                    }
                    phoneCodeDictionary.Remove (model.PhoneNumber);
                    return new UserVerifyResponseResource (model, Key);
                }
            }
            await Task.CompletedTask;
            throw new Exceptions.LoginFaildExeption ();
        }
        private readonly string Key;
        public AuthService (string key, IUserService userService, IMapper mapper, ISmsService smsService, IUnitOfWork unitOfWork) {
            this.Key = key;

            this.mapper = mapper;
            this.smsService = smsService;
            this.unitOfWork = unitOfWork;
            _userService = userService;
        }
    }
}