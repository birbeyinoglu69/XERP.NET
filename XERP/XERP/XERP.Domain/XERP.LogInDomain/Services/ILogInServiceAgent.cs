using System;
namespace XERP.LogInDomain.Services
{
    public interface ILogInServiceAgent
    {
        bool Authenticated(string systemUserID, string password, out string authenticationMessage);
    }
}
