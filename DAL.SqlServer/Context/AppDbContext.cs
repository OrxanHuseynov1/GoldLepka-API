using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Gallery> Galleries{ get; set; }
    public DbSet<Article> Articles{ get; set; }
    public DbSet<Catalog> Catalogs{ get; set; }
    public DbSet<Product> Products{ get; set; }
    public DbSet<Service> Services{ get; set; }
    public DbSet<Designer> Designers{ get; set; }
}
