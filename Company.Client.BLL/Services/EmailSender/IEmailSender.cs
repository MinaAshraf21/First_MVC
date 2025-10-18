using Company.Client.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Services.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
