using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace APITest.Common
{
  public class Utils
  {
    public static string getUserIdFromJWT(string token)
    {
      var jwtParse = parseJWT(token);
      var handler = new JwtSecurityTokenHandler();
      var jwtToken = handler.ReadJwtToken(jwtParse);
      var id = jwtToken.Claims.First(claim => claim.Type == "id").Value;
      return id;
    }

    public static string parseJWT(string token)
    {
      var jwtParse = token.ToString().Replace("Bearer ", "");
      return jwtParse;
    }
  }
}