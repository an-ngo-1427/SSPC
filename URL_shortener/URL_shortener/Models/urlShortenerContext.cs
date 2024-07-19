using Microsoft.EntityFrameworkCore;
namespace URL_shortener.Models;
public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options)
        {

        }
        public DbSet<Url> Urls { get; set; }
    }
