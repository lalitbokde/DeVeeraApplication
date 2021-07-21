
using CRM.Core.TwilioConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.Exceptions;
using Twilio.Rest.Verify.V2.Service;

namespace CRM.Services.TwilioConfiguration
{
    public interface VerificationService:IVerificationService
    {
        
        public async Task<VerificationResult> StartVerificationAsync(string phoneNumber, string channel)
        {
            CRM.Core.TwilioConfig.Twilio twilio = new CRM.Core.TwilioConfig.Twilio();

            try
            {
                var verificationResource = await VerificationResource.CreateAsync(
                    to: phoneNumber,
                    channel: channel,
                    pathServiceSid: twilio.VerificationSid
                );
                return new VerificationResult(verificationResource.Sid);
            }
            catch (TwilioException e)
            {
                return new VerificationResult(new List<string> { e.Message });
            }
        }

        public async Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code)
        {
            CRM.Core.TwilioConfig.Twilio twilio = new CRM.Core.TwilioConfig.Twilio();

            try
            {
                var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: code,
                    pathServiceSid: twilio.VerificationSid
                );
                return verificationCheckResource.Status.Equals("approved") ?
                    new VerificationResult(verificationCheckResource.Sid) :
                    new VerificationResult(new List<string> { "Wrong code. Try again." });
            }
            catch (TwilioException e)
            {
                return new VerificationResult(new List<string> { e.Message });
            }
        }

    }
}
