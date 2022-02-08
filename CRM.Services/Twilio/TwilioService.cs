using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CRM.Data;

namespace CRM.Services.Twilio
{
    public class TwilioService:ITwilioService
    {
        private readonly IRepository<CRM.Core.Domain.Twillio.Twilio> _twilio;
        protected readonly dbContextCRM _dbContext;
        public TwilioService(dbContextCRM dbContext, IRepository<Core.Domain.Twillio.Twilio> twilio)
        {
            this._dbContext = dbContext;
            _twilio = twilio;
        }

        public CRM.Core.Domain.Twillio.Twilio GetAllTwillioConfiguration()
        {
            var query = from a in _twilio.Table
                        orderby a.Id
                        select a;
            var twillioconfig = query.ToList();
            return twillioconfig.FirstOrDefault();
        }



    }
}
