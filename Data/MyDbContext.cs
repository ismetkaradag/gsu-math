using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
    public DbSet<gsu_math.Models.User> User {get;set;}
    public DbSet<gsu_math.Models.Duyuru> Duyuru {get;set;}
    public DbSet<gsu_math.Models.ForumBaslik> ForumBaslik {get;set;}
    public DbSet<gsu_math.Models.ForumCevap> ForumCevap {get;set;}
    public DbSet<gsu_math.Models.Bilgi> Bilgi {get;set;}
    public DbSet<gsu_math.Models.Etkinlik> Etkinlik {get;set;}
    public DbSet<gsu_math.Models.Blog> Blog { get; set; }

}