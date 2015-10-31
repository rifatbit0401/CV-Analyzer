using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using CVAnalyzer.Authentication.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using CVAnalyzer.Repositories;

namespace CVAnalyzer.Authentication.Utility
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly AuthService _authService;
        public string Role;

        public TokenAuthorizeAttribute()
        {
            _authService = new AuthService(new AppContext());
            Role = "";
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string tokenValue = ExtractTokenValue(actionContext.Request.Headers);
            if(tokenValue==null)
            {
                actionContext.Response = new HttpResponseMessage(statusCode: HttpStatusCode.BadRequest);
                return false;
            }

            if(!_authService.IsValidToken(tokenValue))
            {
                actionContext.Response = new HttpResponseMessage(statusCode: HttpStatusCode.Unauthorized);
                return false;
            }

            return true;
        }

        private string ExtractTokenValue(HttpRequestHeaders headers)
        {
            IEnumerable<string> tokenValues;
            headers.TryGetValues("authToken",out tokenValues);
            return tokenValues == null ? null : tokenValues.SingleOrDefault();
        }
    }
}