using System.Data.Entity;
using CVAnalyzer.Authentication.Model;
using CVAnalyzer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVAnalyzer.Authentication.service
{
    public class AuthService
    {
        private readonly AppContext _appContext;

        public AuthService(AppContext appContext)
        {
            _appContext = appContext;
        }


        public string CreateAuthToken(int userId)
        {
            var tokenValue = Guid.NewGuid().ToString();
            var authToken = new AuthToken
            {
                UserId = userId,
                LastAccessTime = DateTime.Now,
                TokenValue = tokenValue
            };

         //   DeleteToken(userId); //uncomment if single signin activation
            
            _appContext.AuthTokens.Add(authToken);
            _appContext.SaveChanges();
            return tokenValue;
        }

        public bool IsValidToken(string tokenValue)
        {
            try
            {
                var authToken = _appContext.AuthTokens.SingleOrDefault(t => t.TokenValue.Equals(tokenValue));
                if (authToken != null && authToken.LastAccessTime.AddDays(2) >= DateTime.Now)
                {
                    UpdateAuthToken(authToken);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void UpdateAuthToken(AuthToken authToken)
        {
            authToken.LastAccessTime = DateTime.Now;
            _appContext.Entry(authToken).State = EntityState.Modified;
            _appContext.SaveChanges();
        }

        public void DeleteToken(int userId)
        {
            IQueryable<AuthToken> authTokens = _appContext.AuthTokens.Where(t => t.UserId.Equals(userId));

            foreach (var authToken in authTokens)
            {
                _appContext.AuthTokens.Remove(authToken);
               
            }
            _appContext.SaveChanges();
        }
    }
}