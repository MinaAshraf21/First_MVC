using Company.Client.DAL.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Client.BLL.Services.Shared.TwilioService
{
    public interface ITwilioService
    {
        MessageResource SendSms(Sms sms);
    }
}
