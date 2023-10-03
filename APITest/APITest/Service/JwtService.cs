using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APITest.Configuration;
using APITest.Models;
using APITest.Service.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APITest.Service
{
  public class JwtService : IJwtService
  {
    private readonly JwtConfig jwtConfig;

    public JwtService(IOptionsMonitor<JwtConfig> optionsMonitor)
    {
      this.jwtConfig = optionsMonitor.CurrentValue;
    }
    public string generateToken(User user)
    {
      var jwtTokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] {
          new Claim( "id", user.id.ToString()),
          new Claim( "email", user.email),
          new Claim( "name", user.name),
        }),
        Expires = DateTime.UtcNow.AddMinutes(15),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = jwtTokenHandler.CreateToken(tokenDescriptor);
      var jwtToken = jwtTokenHandler.WriteToken(token);
      return jwtToken;
    }
  }
}