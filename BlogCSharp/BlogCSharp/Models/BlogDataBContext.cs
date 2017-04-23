namespace BlogCSharp.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class BlogDataBContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDataBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public static BlogDataBContext Create()
        {
            return new BlogDataBContext();
        }
    }
}