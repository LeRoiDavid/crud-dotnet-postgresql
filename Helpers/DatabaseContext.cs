using crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace crud.Helpers;

public class DatabaseContext: DbContext
{
    protected readonly IConfiguration Configuration;


    public DatabaseContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }
    
    
    public DbSet<User> Users { get; set; }

    
}