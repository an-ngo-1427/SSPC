using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using URL_shortener.Models;
using System.Text.Json;
namespace URL_shortener.Controllers;

[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[ApiController]
[Route("[controller]")]
[Route("")]
public class ShortUrlsController : ControllerBase{
    private readonly UrlShortenerContext _context;
    public ShortUrlsController(UrlShortenerContext _context){
        _context = _context;
    }
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public string Get([FromRoute] string id){
        Console.WriteLine("this is a user" + User.FindFirstValue(ClaimTypes.Role));
        // Console.WriteLine(HttpContext.Current.User.Identity);
        // var url = _context.Urls.FirstOrDefault(u=>String.Equals(u.UrlId,id));
        return "testing1";
    }

    [HttpPut]
    public string Create([FromBody] JsonElement data){
        var domainName = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        Console.WriteLine(HttpContext.User);
        Console.WriteLine(data);
        Url newUrl = new(){

        };
        return "testing";
    }

    [HttpPut]
    [Route("{url1}")]
    public string Put([FromBody] CreateUrlRequest data, [FromRoute] string url1){
;        return data.Url;
    }

    // [Route("url1")]
    [HttpDelete]
    [Route("{url1}")]
    public void Delete([FromRoute] string url1){

    }

    [Route("{url1}/hits")]
    [HttpGet]
    public int GetHits([FromRoute] string url1){
        return 4;
    }

    [Route("navigate/ge123")]
    [HttpGet]
    public RedirectResult Navigate(){
        return Redirect("https://google.com");
    }


}
