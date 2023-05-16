using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public bool Visible { get; set; }

        public List<Category>? Categories { get; set; } //many to many


        //costruttore vuoto
        public Photo() { }
    }
}
