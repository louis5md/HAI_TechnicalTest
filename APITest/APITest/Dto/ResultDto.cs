using System;

namespace APITest.Dto
{
  public class ResultDto
  {
    public bool isSuccess { get; set; }
    public string error { get; set; }
    public Object data { get; set; }
  }
}