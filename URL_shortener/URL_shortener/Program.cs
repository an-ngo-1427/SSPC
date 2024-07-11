using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UrlShortner.Helper;
using UrlShortner.Security;
namespace UrlShortner;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["AzureAdB2C:Authority"];
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["AzureAdB2C:Issuer"],
                ValidAudience = builder.Configuration["AzureAdB2C:Audience"]
            };
        })
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        builder.Services.AddAuthorization(options =>
        {
            var permissionsByRoles = DataMock.RolesPermissionsMatrix
                .GroupBy(rolePermission => rolePermission.Permission)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(_group => _group.Role).ToArray());
            foreach (var keyValuePair in permissionsByRoles)
            {
                options.AddPolicy(keyValuePair.Key, policy =>
                    policy.RequireRole(keyValuePair.Value));
            }
        });

        builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformation>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
