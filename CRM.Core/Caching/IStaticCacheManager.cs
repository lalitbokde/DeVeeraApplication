using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Caching
{
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public interface IStaticCacheManager : ICacheManager
    {
    }
}
