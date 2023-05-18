using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [StringLength(75, ErrorMessage = "Il campo può contenere max 60 caratteri.")]
        public string? Subject { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Il campo può contenere da 10 a 2000 caratteri.", MinimumLength = 10)]
        public string Content { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Il campo può contenere max 100 caratteri.")]
        public string Email { get; set; }


        public Message() { }
    }
}
