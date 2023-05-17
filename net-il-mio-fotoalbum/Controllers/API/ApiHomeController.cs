using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Controllers.API
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ApiHomeController : ControllerBase
    //{
    //}

    public class ApiHomeController : Controller
    {
        public IActionResult Index()
        {
            using PhotoContext context = new();
            var photos = context.Photos.Include(p => p.Categories).ToList();

            return View(photos);
        }
    }
}
