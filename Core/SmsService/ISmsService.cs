using System.Threading.Tasks;
namespace Core.SmsService
{
    public interface ISmsService
    {
        Task Send(Resources.SmsResource model);
    }
}