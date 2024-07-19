using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using UrlShortner.Helper;
using URL_shortener.Models;
using UrlShortner.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace UrlShortner;

public class Program
{
    public static void Main(string[] args)
    {
        hashPassword();
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

        builder.Services.AddDbContext<UrlShortenerContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformation>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void hashPassword(){
        SHA256 mySHA256 = SHA256.Create();
        DataMock.Users.ForEach(user=>{
            string passString = user.Password + user.PasswordSalt;
            byte[] hashedPassword =  mySHA256.ComputeHash(Encoding.UTF8.GetBytes(passString));
            user.hashedPassword = Convert.ToBase64String(hashedPassword);
        });
    }
}
