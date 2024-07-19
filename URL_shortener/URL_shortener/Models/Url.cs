using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace URL_shortener.Models;
public class Url{

        public string UrlId { get; set; }
        // [Required]
        public int UserId { get; set; }

        // [StringLength(2000)]
        public string OriginalUrl { get; set; }
        // [Required]
        // [StringLength(50)]
        public string ShortenedUrl { get; set; }

}
