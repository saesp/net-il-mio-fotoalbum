using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Areas;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            using PhotoContext context = new();

            var messages = context.Messages.ToList();

            return View(messages);
        }


        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using PhotoContext context = new();

            var messageDelete = context.Messages.Where(m => m.Id == id).FirstOrDefault();

            if (messageDelete != null)
            {
                context.Messages.Remove(messageDelete);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
