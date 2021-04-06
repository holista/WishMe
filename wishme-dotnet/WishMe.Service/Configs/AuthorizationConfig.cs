using Microsoft.AspNetCore.Authorization;

namespace WishMe.Service.Configs
{
  public class AuthorizationConfig
  {
    public static void SetupRoles(AuthorizationOptions options)
    {
      options.AddPolicy(AuthorizationConstants.Policies._Organizer, policy
        => policy.RequireRole(AuthorizationConstants.Roles._Organizer));
      options.AddPolicy(AuthorizationConstants.Policies._Participant, policy
        => policy.RequireRole(AuthorizationConstants.Roles._Participant, AuthorizationConstants.Roles._Organizer));
    }
  }
}
