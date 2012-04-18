using System;
namespace XERP.Domain.LogInDomain.Services
{
    public interface ILogInServiceAgent
    {
        bool Authenticated(string systemUserID, string password, out string authenticationMessage);
    }
}
