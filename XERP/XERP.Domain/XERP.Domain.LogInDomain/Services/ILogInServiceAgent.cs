using System;
namespace XERP.Domain.LogInDomain.Services
{
    public interface ILogInServiceAgent
    {
        bool Authenticated(string systemUserID, string password, out string authenticationMessage);
        System.Collections.Generic.IEnumerable<XERP.Domain.LogInDomain.LogInDataService.Temp> GetMetaData(string tableName);
    }
}
