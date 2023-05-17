using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_il_mio_fotoalbum.Models
{
    public class CategoryFormModel
    {
        public Category Category { get; set; }

        public List<SelectListItem>? Photos { get; set; }
        public List<string>? SelectPhotos { get; set; }
    }
}
