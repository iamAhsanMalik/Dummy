using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace STM.AIU.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    public ActionResult HelloWorld(string? returnUrl = null)
    {
        returnUrl ??= Url.Content(contentPath: "/");
        return LocalRedirect(returnUrl);
    }
}
