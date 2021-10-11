using System;
namespace Configuration
{
    public  class Twilio
    {
        public string AccountSid { get; set; } = "AC4ee5b4220aa9f74487cb66068dc42702";

        //old token
        //public string AuthToken { get; set; } = "ab95532d32935182d022b369521848d0";
        public string AuthToken { get; set; } = "1b55611ee9b9f72da9e8899e79340edb";
       
        public string VerificationSid { get; set; } = "VA0dc2634edcf87d0df2ff0ae1352e9ef4";
    }
}
