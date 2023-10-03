using APITest.Models;
using Microsoft.EntityFrameworkCore;

namespace APITest.Data
{
  public class DataContext : DbContext, IDataContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}