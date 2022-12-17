using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Lingo_C_BE.Controllers
{
  public class GetWordResult
  {
    public string Word { get; set; } = string.Empty;
  }

  [ApiController]
  [Route("lingo/")]
  public class LingoController : ControllerBase
  {
    [HttpGet("getword")]
    public GetWordResult GetWord()
    {
      GetWordResult result = new GetWordResult();
      using(StreamReader sr = System.IO.File.OpenText(@".\assets\words.txt"))
      {
        string file = sr.ReadToEnd();
        string[] words = Array.ConvertAll(file.Split(','), word => word.ToString());
        result.Word = words.ElementAt<string>(new Random().Next(words.Length - 1));
      }

      return result;
    }
  }
}