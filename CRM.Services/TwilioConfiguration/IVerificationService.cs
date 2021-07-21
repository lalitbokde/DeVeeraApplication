using CRM.Core.TwilioConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.TwilioConfiguration
{
    public interface IVerificationService
    {
        Task<VerificationResult> StartVerificationAsync(string phoneNumber, string channel);

        Task<VerificationResult> CheckVerificationAsync(string phoneNumber, string code);
    }
}
