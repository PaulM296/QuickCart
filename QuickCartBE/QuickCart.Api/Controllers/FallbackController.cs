using Microsoft.AspNetCore.Mvc;

namespace QuickCart.Api.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory
                .GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
