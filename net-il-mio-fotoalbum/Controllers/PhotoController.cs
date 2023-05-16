using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            using (PhotoContext context = new PhotoContext())
            {
                var photos = context.Photos.Include(p => p.Categories).ToList();

                return View(photos);
            }
        }

        
        //CREATE
        [HttpGet]
        public IActionResult Create()
        {
            using (PhotoContext context = new PhotoContext())
            {
                List<Category> categories = context.Categories.ToList();


                //creazione model da passare alla pagina get
                PhotoFormModel model = new PhotoFormModel();

                model.Photo = new Photo();
                // many to many
                List<SelectListItem> listCategories = new();
                foreach (Category category in categories)
                {
                    listCategories.Add(
                        new SelectListItem()
                        {
                            Text = category.Name,
                            Value = category.Id.ToString()
                        }
                    );
                }
                model.Categories = listCategories;


                return View("Create", model);
            }
        }

        [HttpPost]
        public IActionResult Create(PhotoFormModel data)
        {
            using (PhotoContext context = new PhotoContext())
            {
                Photo photoCreate = new();
                photoCreate.Title = data.Photo.Title;
                photoCreate.Description = data.Photo.Description;
                photoCreate.Image = data.Photo.Image;
                photoCreate.Visible = data.Photo.Visible;
                photoCreate.Categories = new List<Category>(); //many to many

                if (data.SelecctCategories != null)
                {
                    foreach (string selectedCategoryId in data.SelecctCategories)
                    {
                        int selectedIntCategoryId = int.Parse(selectedCategoryId); //Parse() converte da un tipo all'altro
                        Category category = context.Categories.Where(m => m.Id == selectedIntCategoryId).FirstOrDefault();

                        photoCreate.Categories.Add(category);
                    }
                }
                context.Photos.Add(photoCreate);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
