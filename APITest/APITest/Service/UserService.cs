using System;
using System.Runtime.ExceptionServices;
using APITest.Common;
using APITest.Converter.Interface;
using APITest.Dto;
using APITest.Models;
using APITest.Repository.Interface;
using APITest.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITest.Service
{
  public class UserService : IUserService
  {
    private readonly IUserRepository userRepository;
    private readonly IUserConverter userConverter;
    private readonly IJwtService jwtService;

    public UserService(IUserRepository userRepository,
                      IUserConverter userConverter,
                      IJwtService jwtService)
    {
      this.userRepository = userRepository;
      this.userConverter = userConverter;
      this.jwtService = jwtService;
    }
    public async Task<UserDto> register(RegistrationDto registrationDto)
    {
      try
      {
        User user = await userRepository.findByEmail(registrationDto.email);
        if (user != null)
        {
          throw new Exception("User already exist.");
        }
        registrationDto.password = BCrypt.Net.BCrypt.HashPassword(registrationDto.password);
        UserDto userDto = new UserDto();
        userDto.email = registrationDto.email;
        userDto.password = registrationDto.password;
        userDto.name = registrationDto.name;

        user = await userRepository.save(userConverter.convertDtoToModel(userDto));

        UserDto result = userConverter.convertModelToDto(user);

        var jwtToken = jwtService.generateToken(user);
        result.token = jwtToken;

        return result;
      }
      catch (Exception e)
      {
        ExceptionDispatchInfo.Capture(e).Throw();
        throw;
      }
    }
    public async Task<UserDto> login(LoginDto userDto)
    {
      try
      {
        User user = await userRepository.findByEmail(userDto.email);
        if (user == null)
        {
          throw new Exception("email or password wrong");
        }
        if (!BCrypt.Net.BCrypt.Verify(userDto.password, user.password))
        {
          throw new Exception("email or password wrong");
        }

        UserDto result = userConverter.convertModelToDto(user);

        var jwtToken = jwtService.generateToken(user);
        result.token = jwtToken;

        return result;
      }
      catch (Exception e)
      {
        ExceptionDispatchInfo.Capture(e).Throw();
        throw;
      }
    }

    public async Task<UserDto> auth(string token)
    {
      try
      {
        if (token == null)
        {
          throw new Exception("User not authorized.");
        }

        var id = Utils.getUserIdFromJWT(token);
        User user = await userRepository.findById(id);
        if (user == null)
        {
          throw new Exception("User not found.");
        }

        UserDto result = userConverter.convertModelToDto(user);
        result.token = token;

        return result;
      }
      catch (Exception e)
      {
        ExceptionDispatchInfo.Capture(e).Throw();
        throw;
      }
    }

    public async Task<Guid> getUserId(string token)
    {
      if (token == null)
      {
        throw new Exception("User not authorized.");
      }

      var id = Utils.getUserIdFromJWT(token);
      User user = await userRepository.findById(id);
      if (user == null)
      {
        throw new Exception("User not authorized.");
      }
      return user.id;
    }

    public async Task<UserDto> updateUser(string token, UpdateUserDto userDto)
    {
      try
      {
        if (token == null)
        {
          throw new Exception("User not authorized.");
        }

        var id = Utils.getUserIdFromJWT(token);
        User user = await userRepository.findById(id);
        if (user == null)
        {
          throw new Exception("User not authorized.");
        }

        if (userDto.name != null)
        {
          user.name = userDto.name;
        }
        if (userDto.password != null)
        {
          user.password = BCrypt.Net.BCrypt.HashPassword(userDto.password); ;
        }

        user = await userRepository.update(user);

        return userConverter.convertModelToDto(user);

      }
      catch (Exception e)
      {
        ExceptionDispatchInfo.Capture(e).Throw();
        throw;
      }

    }

    public async Task<UserDto> deleteUser(string token)
    {
        try
        {
            if (token == null)
            {
                throw new Exception("User not authorized.");
            }

            var id = Utils.getUserIdFromJWT(token);
            User user = await userRepository.findById(id);
            if (user == null)
            {
                throw new Exception("User not authorized.");
            }

            user = await userRepository.delete(user);

            return userConverter.convertModelToDto(user);
        }
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e).Throw();
            throw;
        }
    }

    public async Task<List<UserDto>> getSimilarUsers(string email)
    {
        try
        {
            List<User> users = await userRepository.findSimilarUsersByEmail(email);
       
            return userConverter.convertListModelToListDto(users);
        }
        catch (Exception e)
        {
            ExceptionDispatchInfo.Capture(e).Throw();
            throw;
        }
    }
  }
}