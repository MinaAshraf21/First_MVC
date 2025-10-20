using Company.Client.DAL.Common.Entities;

namespace Company.Client.BLL.Services.Shared.MailService
{
    public interface IMailService
    {
        void SendEmail(Email email);
    }
}
