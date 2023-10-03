using APITest.Models;

namespace APITest.Service.Interface
{
  public interface IJwtService
  {
    public string generateToken(User user);
  }
}