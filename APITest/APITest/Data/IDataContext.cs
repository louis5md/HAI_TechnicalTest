using APITest.Models;
using Microsoft.EntityFrameworkCore;

namespace APITest.Data
{
  public interface IDataContext
  {
    DbSet<User> Users { get; set; }

  }
}