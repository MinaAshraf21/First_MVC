using Company.Client.DAL.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.BLL.Services.Shared.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
