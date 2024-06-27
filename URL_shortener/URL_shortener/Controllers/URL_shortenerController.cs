using Microsoft.AspNetCore.Mvc;

namespace URL_shortener.Controllers;


[ApiController]
[Route("[controller]")]
[Route("")]
public class ShortUrlsController : ControllerBase{
    [HttpGet]
    [Route("{url1}")]
    public string Get([FromRoute] string url1){
        Console.WriteLine(url1);
        return "http://localhost:5097/navigate/ge123";
    }

    [HttpPut]
    public string Create([FromBody] CreateUrlRequest data){
;        return data.Url;
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
