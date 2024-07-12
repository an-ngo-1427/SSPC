using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using UrlShortner.Helper;

namespace UrlShortner.Security;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization!);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var email = credentials[0];
            var password = credentials[1];

            var user = DataMock.Users.FirstOrDefault(user => user.Email!.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (hashPassword(password, user.PasswordSalt) == user.hashedPassword)
            {
                var claims = new[] { new Claim("emails", email) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
    public static string hashPassword(string password, string salt){
        SHA256 mySHA = SHA256.Create();
        byte[] bytePass = Encoding.UTF8.GetBytes(password + salt);
        byte[] hashedPass = mySHA.ComputeHash(bytePass);
        return Convert.ToBase64String(hashedPass);
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}
