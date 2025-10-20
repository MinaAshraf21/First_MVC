using Company.Client.DAL.Common.Entities;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Client.BLL.Services.Shared.TwilioService
{
    public class TwilioService(IOptions<TwilioSettings> _options) : ITwilioService
    {
        public MessageResource SendSms(Sms sms)
        {
            //Initialize Connection
            //connect to twilio server to use twilio services
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            //Build Message
            //we cannot create an instance from MessageResource to create the sms message ,
            //  so we are requesting to twilio to create the message and validate it
            var message = MessageResource.Create
                (
                    body: sms.Body,
                    to: sms.To,
                    from: _options.Value.PhoneNumber
                    //from: new Twilio.Types.PhoneNumber(_options.Value.PhoneNumber)
                );
            
            //Return SMS Message (MessageResource)
            return message;
        }
    }
}
