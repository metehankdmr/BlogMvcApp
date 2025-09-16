using System.ComponentModel.DataAnnotations;

namespace BlogMvcApp.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Başlık gereklidir")]
        [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = "";
        
        [Required(ErrorMessage = "İçerik gereklidir")]
        [Display(Name = "İçerik")]
        public string Content { get; set; } = "";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
