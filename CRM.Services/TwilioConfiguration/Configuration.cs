using System;
namespace Configuration
{
    public  class Twilio
    {
        public string AccountSid { get; set; } = "AC4ee5b4220aa9f74487cb66068dc42702";

        //old token
        //public string AuthToken { get; set; } = "ab95532d32935182d022b369521848d0";
        public string AuthToken { get; set; } = "2bcfb92abec88d6c5a043e8c5a13ae31";
       
        public string VerificationSid { get; set; } = "VA0dc2634edcf87d0df2ff0ae1352e9ef4";
    }
}
