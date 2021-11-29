
using CRM.Core.TwilioConfig;
using CRM.Data.Interfaces;
using CRM.Services.Twilio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Verify.V2.Service;

namespace CRM.Services.TwilioConfiguration
{
    public class VerificationService : IVerificationService
    {

       // private readonly Configuration.Twilio _config;
        private readonly ITwilioService _ITwilioService;

        public VerificationService(ITwilioService ITwilioService)
        {
            //_config = configuration;
            //TwilioClient.Init(_config.AccountSid, _config.AuthToken);
            this._ITwilioService = ITwilioService;
        }


        
        public async Task<VerificationResult> StartVerificationAsync(string phoneNumber, string channel)
        {
            try
            {
                var verficationsid = _ITwilioService.GetAllTwillioConfiguration();
                var verificationResource = VerificationResource.Create(
                    to: phoneNumber,
                    channel: channel,
                    pathServiceSid: verficationsid.VerificationSid

                );
                Console.WriteLine(verificationResource.Status);
                return new VerificationResult(verificationResource.Sid);
            }
            catch (TwilioException e)
            {
                
                return new VerificationResult(new List<string> { e.Message });
            }
        }

        public async Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code)
        {
            try
            {
                var verficationsid = _ITwilioService.GetAllTwillioConfiguration();
                var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                    to: phoneNumber,
                    code: code,
                    pathServiceSid: verficationsid.VerificationSid
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


        //public  Twilio GetTwillioConfiguration(int LayoutSetupId)
        //{
        //    if (LayoutSetupId == 0)
        //        return null;


        //    return _LayoutSetupRepository.GetById(LayoutSetupId);
        //}
    }
}
