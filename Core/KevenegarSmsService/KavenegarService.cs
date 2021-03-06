using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.KevenegarSmsService.Configuration;
using Core.SmsService;
using Core.SmsService.Resources;
using Microsoft.Extensions.Options;

namespace Core.KevenegarSmsService {
    public class KavenegarService : ISmsService {
        private readonly string key;

        public async Task Send (SmsResource model) {
            string url = $"https://api.kavenegar.com/v1/{this.key}/sms/send.json";
            var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(new []{
                new KeyValuePair<string,string>("receptor",model.PhoneNumber),
                new KeyValuePair<string,string>("message",model.Context)
            });
            var resp = await httpClient.PostAsync(url,content);
            if(resp.StatusCode != HttpStatusCode.OK){
                throw new System.Exception(await resp.Content.ReadAsStringAsync());
            }
        }
        

        public KavenegarService (string key) {
            this.key = key;
        }
    }
}