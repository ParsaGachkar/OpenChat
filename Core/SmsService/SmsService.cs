using System.Threading.Tasks;
using Core.SmsService.Resources;
using Microsoft.Extensions.Logging;

namespace Core.SmsService
{
    public class SmsService : ISmsService
    {
        public SmsService(ILogger<SmsService> logger)
        {
            this.logger = logger;
        }
        private ILogger<SmsService> logger;
        public async virtual Task Send(SmsResource model)
        {
            logger.LogInformation($"Sending '{model.Context}' to '{model.PhoneNumber}'");
            await Task.CompletedTask;
            //throw new System.NotImplementedException();
        }
    }
}