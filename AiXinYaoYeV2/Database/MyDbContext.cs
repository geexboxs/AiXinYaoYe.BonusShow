using Microsoft.EntityFrameworkCore;

namespace AiXinYaoYeV2.Database
{

    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        :base(options)
        {
            
        }
        public virtual DbSet<Entity.BonusProduct> BonusProducts { get; set; }
        public virtual DbSet<Entity.RecommandProduct> RecommandProducts { get; set; }
        public virtual DbSet<Entity.Admin> Admins { get; set; }
    }
}