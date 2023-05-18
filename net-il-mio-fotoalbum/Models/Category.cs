using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il campo può contenere max 100 caratteri.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Il campo può contenere da 2 a 1000 caratteri.", MinimumLength = 2)]
        public string? Description { get; set; }

        public List<Photo>? Photos { get; set; } //many to many


        //costruttore vuoto
        public Category() { }
    }
}
