using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using URL_shortener.Models;
using System.Text.Json;
using System.Security.Claims;
namespace URL_shortener.Controllers;

[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[ApiController]
[Route("[controller]")]

public class ShortUrlsController : ControllerBase
{
    private readonly UrlShortenerContext _context;
    public ShortUrlsController(UrlShortenerContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet("{id}")]
    public Url Get([FromRoute] string id)
    {
        var url = _context.Urls.SingleOrDefault(u => u.UrlId == id);
        if (url == null)
        {
            return null;
        }
        else
        {
            return url;
        }
    }

    [Authorize]
    [HttpPut("{id}")]

    public string Put([FromBody] JsonElement body, [FromRoute] string id)
    {

        var domainName = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        Console.WriteLine($"request to create: {id}, {body.GetProperty("url")}");
        Url url = new()
        {
            OriginalUrl = body.GetProperty("url").ToString(),
            ShortenedUrl = $"{domainName}/navigate/{Guid.NewGuid()}",
            UrlId = id,
            UserId = 0
        };

        _context.Urls.Add(url);
        _context.SaveChanges();

        return url.ShortenedUrl;
    }

    // [Route("url1")]
    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public string Delete([FromRoute] string id)
    {
        Console.WriteLine($"request to delete: {id}");

        var url = _context.Urls.FirstOrDefault(u => string.Equals(u.UrlId, id));
        if (url != null)
        {
            _context.Remove(url);
            _context.SaveChanges();
            return "deleted!";
        }

        return "not found!";
    }
    [Authorize]
    [Route("{url1}/hits")]
    [HttpGet]
    public int GetHits([FromRoute] string url1)
    {
        return 4;
    }
    [AllowAnonymous]
    [Route("{userShort}")]
    [HttpGet]
    public RedirectResult Navigate(string userShort)
    {
         var url = _context.Urls.SingleOrDefault(u => u.ShortenedUrl.Contains(userShort));

        return url == null ? Redirect("https://www.google.com") : Redirect(url.OriginalUrl);
    }
}
