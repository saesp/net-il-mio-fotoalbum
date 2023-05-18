using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        //queste validazioni sono sia limitazioni per il db sia vincoli per l'user
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(200, ErrorMessage = "Il campo può contenere max 200 caratteri.")]
        public string Title { get; set; }

        [StringLength(1500, ErrorMessage = "Il campo può contenere max 1500 caratteri.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        [StringLength(2000, ErrorMessage = "Il campo può contenere max 2000 caratteri.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio.")]
        public bool Visible { get; set; }

        public List<Category>? Categories { get; set; } //many to many


        //costruttore vuoto
        public Photo() { }
    }
}
