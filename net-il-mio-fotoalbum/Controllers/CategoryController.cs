using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            using PhotoContext context = new();

            var categoriesList = context.Categories.ToList();

            return View(categoriesList);
        }


        //CREATE
        [HttpGet]
        public IActionResult Create()
        {
            using PhotoContext context = new();

            //creazione model da passare alla pagina get
            CategoryFormModel model = new();

            model.Category = new Category();

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            using PhotoContext context = new PhotoContext();

            Category categoryCreate = new();
            categoryCreate.Name = data.Category.Name;
            categoryCreate.Description = data.Category.Description;

            context.Categories.Add(categoryCreate);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) 
        {
            using PhotoContext context = new PhotoContext();
            var categoryDelete = context.Categories.Where(c => c.Id == id).FirstOrDefault();

            if (categoryDelete != null)
            {
                context.Categories.Remove(categoryDelete);
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
