using Microsoft.AspNetCore.Authorization;

namespace HubCommerce.AuthorizationServices
{
    public class RestrictBannedUserHandler : AuthorizationHandler<RestrictBannedUserRequirments>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestrictBannedUserRequirments requirement)
        {
            if (context.User.HasClaim("IsBanned", "true"))
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
