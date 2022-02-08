using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.Twillio
{
    public class Twilio : BaseEntity
    {
        public string AccountSid { get; set; }// = "AC4ee5b4220aa9f74487cb66068dc42702";
        public string AuthToken { get; set; } //= "aa4c0f63b85fd3dba1759122892e7ee9";
        public string VerificationSid { get; set; }// = "VA0dc2634edcf87d0df2ff0ae1352e9ef4";
    }
}
