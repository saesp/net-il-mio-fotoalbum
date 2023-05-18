using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Areas;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using Message = net_il_mio_fotoalbum.Models.Message;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiHomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() // localhost:7250/API/ApiHome/Index
        {
            using PhotoContext context = new();

            IQueryable<Photo> photos = context.Photos;

            //var photos = context.Photos.Include(p => p.Categories).ToList();

            return Ok(photos.ToList());
        }


        [HttpPost] // localhost:7250/api/ApiHome/CreateMessage //no token
        public IActionResult CreateMessage([FromBody] Message data)
        {
            using PhotoContext context = new();

            Message messageCreate = new();
            messageCreate.Subject = data.Subject;
            messageCreate.Content = data.Content;
            messageCreate.Email = data.Email;

            context.Messages.Add(messageCreate);
            context.SaveChanges();

            return Ok();
        }
    }
}
