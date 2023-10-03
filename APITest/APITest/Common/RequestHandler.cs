using System;
using System.Threading.Tasks;
using APITest.Dto;

namespace APITest.Common
{
  public class RequestHandler
  {
    public ResultDto getResult(Func<Object> processRequest)
    {
      ResultDto result = new ResultDto();
      try
      {
        Object obj = processRequest();
        if (!obj.Equals(null))
        {
          result.data = obj;
          result.isSuccess = true;
          result.error = null;
        }
        else
        {
          result.data = null;
          result.isSuccess = false;
          result.error = "Request failed";
        }
      }
      catch (Exception e)
      {
        result.data = null;
        result.isSuccess = false;
        result.error = e.Message;
      }
      return result;
    }
  }

  public class RequestHandlerAsync<T>
  {
    public async Task<ResultDto> getResultAsync(Func<Task<T>> processRequest)
    {
      ResultDto result = new ResultDto();
      try
      {
        T obj = await Task.Run<T>(() => processRequest());
        if (!obj.Equals(null))
        {
          result.data = obj;
          result.isSuccess = true;
          result.error = null;
        }
        else
        {
          result.data = null;
          result.isSuccess = false;
          result.error = "Request failed";
        }
      }
      catch (Exception e)
      {
        result.data = null;
        result.isSuccess = false;
        result.error = e.Message;
      }
      return result;
    }
  }
}