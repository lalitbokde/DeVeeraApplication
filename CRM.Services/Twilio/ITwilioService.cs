using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Twilio
{
    public interface ITwilioService
    {
        public CRM.Core.Domain.Twillio.Twilio GetAllTwillioConfiguration();
    }
}
