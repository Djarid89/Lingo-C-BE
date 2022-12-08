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
    private static readonly ImmutableHashSet<string> Words = ImmutableHashSet.Create<string>(new string[] {
      "Statua", "Culo", "Orgoglio", "Ceppa", "Suca", "Rolly", "Gamba", "Anno", "Buono", "Cattivo"
    });

    [HttpGet("getword")]
    public GetWordResult GetWord()
    {
      GetWordResult result = new GetWordResult();
      result.Word = Words.ElementAt<string>(new Random().Next(Words.Count - 1));

      return result;
    }
  }
}