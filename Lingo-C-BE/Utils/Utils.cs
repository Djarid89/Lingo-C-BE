using System.Globalization;
using System.Text;

namespace Lingo_C_BE.Utils
{
  public static class Utils
  {
    public static string RemoveAccents(this string text)
    {
      StringBuilder sbReturn = new StringBuilder();
      var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
      foreach(char letter in arrayText)
      {
        if(CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
          sbReturn.Append(letter);
      }
      return sbReturn.ToString();
    }
  }
}
