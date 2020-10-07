using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace API.BackgroundTasks
{
    public class InternalApiTokenAuthorizationHandler : AuthorizationHandler<InternalApiTokenRequirement>
    {
        private const string AuthorizationHeaderKey = "Authorization";
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public InternalApiTokenAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InternalApiTokenRequirement requirement)
        {
            var token = configuration.GetValue<string>("BackgroundTask:AuthorizationToken");

            if (IsAuthenticated(httpContextAccessor.HttpContext.Request.Headers, token))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private static bool IsAuthenticated(IHeaderDictionary headers, string token)
            => headers.TryGetValue(AuthorizationHeaderKey, out var authorizationHeader)
               && authorizationHeader.Count > 0
               && !string.IsNullOrEmpty(authorizationHeader[0])
               && token == authorizationHeader[0];
    }

    public class InternalApiTokenRequirement : IAuthorizationRequirement
    {
    }
}
