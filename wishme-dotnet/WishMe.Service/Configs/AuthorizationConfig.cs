using Microsoft.AspNetCore.Authorization;

namespace WishMe.Service.Configs
{
  public class AuthorizationConfig
  {
    public static void SetupRoles(AuthorizationOptions options)
    {
      string organizer = UserRole.Organizer.ToString();
      string participant = UserRole.Participant.ToString();

      options.AddPolicy(organizer, policy => policy.RequireRole(organizer));
      options.AddPolicy(participant, policy => policy.RequireRole(participant, organizer));
    }
  }
}
