using Microsoft.AspNetCore.Mvc;

namespace Lingo_C_BE.Controllers
{
  public class GetWordResult
  {
    public string Word { get; set; } = string.Empty;
  }

  public class CheckWordResult
  {
    public bool Valid { get; set; }
  }

  [ApiController]
  [Route("lingo/")]
  public class LingoController : ControllerBase
  {
    [HttpGet("getword/{size}")]
    public GetWordResult GetWord(int? size = null)
    {
      GetWordResult result = new GetWordResult();
      using(StreamReader sr = System.IO.File.OpenText(@".\assets\lingoWords.txt"))
      {
        List<string> words = new List<string>(sr.ReadToEnd().Split(','));
        if(size != null)
        {
          words = words.Where(word => word.Length == size).ToList();
        }
        result.Word = words.ElementAt(new Random().Next(words.Count - 1));
      }
      

      return result;
    }

    [HttpGet("checkword/{word}")]
    public CheckWordResult CheckWord(string word)
    {
      CheckWordResult result = new CheckWordResult();
      using(StreamReader sr = System.IO.File.OpenText(@".\assets\words.txt"))
      {
        List<string> words = new List<string>(sr.ReadToEnd().Split(','));
        result.Valid = words.Exists(w => w.ToUpper().Equals(word.ToUpper()));
      }

      return result;
    }
  }
}