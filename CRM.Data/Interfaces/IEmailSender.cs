using System.Threading.Tasks;

namespace CRM.Data.Interfaces
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
