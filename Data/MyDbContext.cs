using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
    public DbSet<gsu_math.Models.User> User {get;set;}
    public DbSet<gsu_math.Models.Yetki> Yetki {get;set;}
}