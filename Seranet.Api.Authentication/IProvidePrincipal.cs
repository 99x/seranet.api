using System.Security.Principal;

namespace Seranet.Api.Authentication
{
    public interface IProvidePrincipal
    {
        IPrincipal CreatePrincipal(string username, string password);
    }
}