using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AiXinYaoYe.Database
{

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Entity.BonusProduct> BonusProducts { get; set; }
        public virtual DbSet<Entity.RecommandProduct> RecommandProducts { get; set; }
        public virtual DbSet<Entity.Admin> Admins { get; set; }
    }
}