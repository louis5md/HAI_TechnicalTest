using System;
using APITest.Converter.Interface;
using APITest.Dto;
using APITest.Models;
using System.Collections.Generic;

namespace APITest.Converter
{
  public class UserConverter : IUserConverter
  {
    public User convertDtoToModel(UserDto dtos)
    {
      return new User
      {
        name = dtos.name,
        email = dtos.email,
        password = dtos.password
      };
    }

    public UserDto convertModelToDto(User model)
    {
      return new UserDto
      {
        name = model.name,
        email = model.email
      };
    }

    public List<User> convertListDtoToListModel(List<UserDto> dtos)
    {
        List<User> list = new List<User>();
        foreach (var dto in dtos)
        {
            list.Add(convertDtoToModel(dto));
        }
        return list;
    }

    public List<UserDto> convertListModelToListDto(List<User> models)
    {
        List<UserDto> list = new List<UserDto>();
        foreach (var model in models)
        {
            list.Add(convertModelToDto(model));
        }
        return list;
    }
  }
}