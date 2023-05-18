using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Areas;
using net_il_mio_fotoalbum.Models;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "SUPERADMIN,ADMIN")] //autorizzazione per tutto
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }
            using (PhotoContext context = new PhotoContext())
            {
                Photo photoCreate = new();
                photoCreate.Title = data.Photo.Title;
                photoCreate.Description = data.Photo.Description;
                photoCreate.Image = data.Photo.Image;
                photoCreate.Visible = data.Photo.Visible;
                photoCreate.Categories = new List<Category>(); //many to many

                if (data.SelectCategories != null)
                {
                    foreach (string selectedCategoryId in data.SelectCategories)
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


        //READ
        [HttpGet]
        public IActionResult Read(int id)
        {
            using (PhotoContext context = new PhotoContext())
            {
                var photo = context.Photos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();

                if (photo != null)
                {
                    return View(photo);
                }
                else
                {
                    return NotFound(); 
                }
            }
        }


        //UPDATE
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PhotoContext context = new PhotoContext())
            {
                //var photoUpdate = context.Photos.Include(m => m.Categories).FirstOrDefault(p => p.Id == id);
                var photoUpdate = context.Photos.Where(p => p.Id == id).Include(m => m.Categories).FirstOrDefault();

                List<Category> categories = context.Categories.ToList();

                //creazione model da passare alla pagina get
                PhotoFormModel model = new();

                model.Photo = photoUpdate;
                List<SelectListItem> listCategories = new(); //many to many
                foreach (Category category in categories)
                {
                    listCategories.Add(
                        new SelectListItem()
                        {
                            Text = category.Name,
                            Value = category.Id.ToString(),
                            Selected = photoUpdate.Categories.Any(m => m.Id == category.Id) //Any() restituisce un valore booleano che indica se l'enumerazione soddisfa una determinata condizione
                        }
                    );
                }
                model.Categories = listCategories;


                return View("Update", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", data);
            }

            using (PhotoContext context = new())
            {
                var photoUpdate = context.Photos.Where(p => p.Id == id).Include(m => m.Categories).FirstOrDefault();

                if(photoUpdate != null)
                {
                    photoUpdate.Title = data.Photo.Title;
                    photoUpdate.Description = data.Photo.Description;
                    photoUpdate.Image = data.Photo.Image;
                    photoUpdate.Visible = data.Photo.Visible;
                    photoUpdate.Categories = new List<Category>(); //many to many

                    if (data.SelectCategories != null)
                    {
                        photoUpdate.Categories.Clear(); //Clear() rimuove tutti gli elementi in una lista (in quest caso permette di togliere la selezione degli ingredienti)

                        foreach (string selectedCategoryId in data.SelectCategories)
                        {
                            int selectedIntCategoryId = int.Parse(selectedCategoryId); //Parse() converte da un tipo all'altro
                            Category category = context.Categories.Where(m => m.Id == selectedIntCategoryId).FirstOrDefault();

                            photoUpdate.Categories.Add(category);
                        }
                    }

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }


        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PhotoContext context = new PhotoContext())
            {
                Photo photoDelete = context.Photos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();

                if (photoDelete != null)
                {
                    context.Photos.Remove(photoDelete);
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
}
