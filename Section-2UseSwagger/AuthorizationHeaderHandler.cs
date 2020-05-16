using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Section_2UseSwagger
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
         HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> apiKeyHeaderValues = null;

            if (request.Headers.TryGetValues("ApiKey", out apiKeyHeaderValues))
            {
                var apiKeyHeaderValue = apiKeyHeaderValues.First();

                // ... your authentication logic here ...
                var username = (apiKeyHeaderValue == "12345" ? "Maarten" : "OtherUser");

                if(apiKeyHeaderValue == "12345")
                {
                   // return request.he
                }

                var usernameClaim = new Claim(ClaimTypes.Name, username);
                var identity = new ClaimsIdentity(new[] { usernameClaim }, "ApiKey");
                var principal = new ClaimsPrincipal(identity);

                Thread.CurrentPrincipal = principal;
            }

            return base.SendAsync(request, cancellationToken);           

        }
    }
}