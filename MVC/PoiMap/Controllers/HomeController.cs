using Microsoft.AspNetCore.Mvc;

namespace PoiMap.Controllers;

public class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View("~/Views/Shared/Error.cshtml");
}
