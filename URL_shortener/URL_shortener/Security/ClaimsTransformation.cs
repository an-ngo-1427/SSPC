using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using UrlShortner.Helper;

namespace UrlShortner.Security;

public class ClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;

        var email = principal.FindFirstValue("emails");
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new UnauthorizedAccessException("Email not provided");
        }

        var user = DataMock.GetUserByEmail(email)
                ?? throw new UnauthorizedAccessException("Invalid user!");

        var newClaims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role));

        identity!.AddClaims(newClaims);

        return Task.FromResult(principal);
    }
}
