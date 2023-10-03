using APITest.Dto;
using System.Collections.Generic;
using APITest.Models;
using System.Threading.Tasks;
using System;

namespace APITest.Service.Interface
{
  public interface IUserService
  {
    public Task<UserDto> register(RegistrationDto dto);
    public Task<UserDto> login(LoginDto dto);
    public Task<UserDto> auth(string token);
    public Task<Guid> getUserId(string token);
    public Task<UserDto> updateUser(string token, UpdateUserDto dto);
    public Task<UserDto> deleteUser(string token);
    public Task<List<UserDto>> getSimilarUsers(string email);
  }
}