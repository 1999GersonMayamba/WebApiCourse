using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Section6_Authorization.AuthFilters
{
    public class AuthenticationBearerBodyApi_Key : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// Set to the Authorization header Scheme value that this filter is intended to support
        /// </summary>
        public const string SupportedTokenScheme = "Bearer";

        /// <summary>
        /// Set Api key have existe in value of Bearer
        /// </summary>
        public const string Api_Key = "OfED+KgbZxtu4e4+JSQWdtSgTnuNixKy1nMVAEww8QL3";

        public const string _apiKeyHeader = "ApiKey";

        public bool AllowMultiple { get { return false; } }

        public bool SendChallenge { get; set; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            //for get aout a value contains in custom header X-API-Key
            IEnumerable<string> apiKeyHeaderValues = null;

            // STEP 1: extract your credentials from the request.  
            var authHeader = context.Request.Headers.Authorization;
            var CustomHeaderKeyValue = context.Request.Headers.TryGetValues("X-API-Key", out apiKeyHeaderValues);


            // if there are no credentials or in header not have a X-API-Key value, abort out
            //if (authHeader == null && CustomHeaderKeyValue == false)
            //return;


            // STEP 2: Given a valid token scheme, verify credentials are present
            var credentials = authHeader.Parameter;
            if (!String.IsNullOrEmpty(credentials))
            {
                //String for read a value ignore a ApiKey text
                string Api_Key_Authorization_Header = credentials.Substring(11, 7);

                IPrincipal principal = await ValidateCredentialsAsync(Api_Key_Authorization_Header, context.Request, cancellationToken);
                if (principal == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid security token value", context.Request);
                }
                else
                {
                    // We have a valid, authenticated user; save off the IPrincipal instance
                    context.Principal = principal;
                }
            }

            //If have value in custom header X-API-Key
            if (context.Request.Headers.TryGetValues("X-API-Key", out apiKeyHeaderValues))
            {

                string apiKeyHeaderValue = apiKeyHeaderValues.First();

                IPrincipal principal = await ValidateCredentialsAsync(apiKeyHeaderValue, context.Request, cancellationToken);
                if (principal == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid security token value", context.Request);
                }
                else
                {
                    // We have a valid, authenticated user; save off the IPrincipal instance
                    context.Principal = principal;
                }
            }


        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            // if this filter wants to support WWW-Authenticate header challenges, add one to the
            // result
            if (SendChallenge)
            {

                context.Result = new AddChallengeOnUnauthorizedResult(
                    new AuthenticationHeaderValue(SupportedTokenScheme),
                    context.Result);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Internal method to validate the credentials included in the request,
        /// returning an IPrincipal for the resulting authenticated entity.
        /// </summary>
        private async Task<IPrincipal> ValidateCredentialsAsync(string credentials,
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            if (credentials.Length == 8 && credentials.Substring(0, 3) == "123" && credentials.Substring(3, 5) == "ABCDE")
            {
                IList<Claim> claimCollection = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Api_Key),
                    new Claim(ClaimTypes.AuthenticationInstant, "10"),
                    new Claim("urn:ClientAccount", Api_Key.Substring(0, 3))

                };

                var identity = new ClaimsIdentity(claimCollection, SupportedTokenScheme);
                var principal = new ClaimsPrincipal(identity);

                return await Task.FromResult(principal);
            }
            else
            {
                //if the credencial not correspond the logic validation return null
                return null;
            }

        }
    }
}