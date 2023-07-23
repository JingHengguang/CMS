using Cms.Backend.Api.Model;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

namespace Cms.Backend.Db
{
    public class CmsBackendDb : DbContext
    {
        public IConfiguration Configuration { get; }
        public CmsBackendDb(){}

        /// <summary>
        /// 采用依赖注入的方式的时，必须提供其构造函数，并且调用其父类构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public CmsBackendDb(DbContextOptions options) : base(options)
        {
            // Configuration.ProxyCreationEnabled=false;
        }
        
        // 实例化数据表
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Broadcast_map> Broadcast_map { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Contents> Contents { get; set; }
        public DbSet<Corporate_information> Corporate_information { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<AuditInfos> AuditInfos { get; set; }



        //添加种子数据, 数据库  PostgreDbContext相关配置
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}