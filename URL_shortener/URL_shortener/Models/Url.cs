public class Url{

        public string UrlId { get; set; }

        public int UserId { get; set; }

        // [StringLength(2000)]
        public string OriginalUrl { get; set; }
        // [Required]
        // [StringLength(50)]
        public string ShortenedUrl { get; set; }

}
