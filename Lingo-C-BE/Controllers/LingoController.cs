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

  public class UpdateWordsData
  {
    public List<string> Words { get; set; }
  }

  public class UpdateWordsResult
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

    [HttpPost("updatewords")]
    public UpdateWordsResult UpdateWords(UpdateWordsData data)
    {
      UpdateWordsResult result = new UpdateWordsResult();
      string path = @".\assets\lingoWords.txt";
      FileStream fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
      List<string> words = new List<string>();
      using(StreamReader sr = new StreamReader(fs))
      {
        words = new List<string>(sr.ReadToEnd().Split(','));
        fs.Dispose();
      }

      using(StreamWriter sw = System.IO.File.AppendText(path))
      {
        data.Words.ForEach(word =>
        {
          if(!words.Exists(w => w.ToUpper().Equals(word.ToUpper())) && CheckWord(word).Valid)
          {
            sw.Write(',' + word);
          }
        });
      }

      result.Valid = true;
      return result;
    }
  }
}