using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using practice_CMS_backend.Authentication;

namespace practice_CMS_backend.CMS
{
    public class CMSDbContext : IdentityDbContext<User>
    {

        public DbSet<PostModel> posts { get; set; }

        public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}