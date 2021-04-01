using System.Security.Principal;

namespace CRM.Data.Interfaces
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
