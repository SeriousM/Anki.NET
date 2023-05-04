using System.Security.Cryptography;
using System.Text;

namespace AnkiNet.mapper;

public class ChecksumUtils
{
  public static long Checksum(string data)
  {
    // Kotlin code from https://github.com/ankidroid/Anki-Android/blob/e7a9acfd7657221fe59ef24a6eb4027614cc9286/AnkiDroid/src/main/java/com/ichi2/libanki/Utils.kt#L554

    string result;
    try
    {
      var digest = SHA1.HashData(Encoding.UTF8.GetBytes(data));

      result = string.Concat(digest.Select(b => b.ToString("X2")).ToArray());

      // pad with zeros to length of 40 This method used to pad
      // to the length of 32. As it turns out, sha1 has a digest
      // size of 160 bits, leading to a hex digest size of 40,
      // not 32.
      if (result.Length < 40)
      {
        var zeroes = new string('0', 40 - result.Length);
        result = zeroes + result;
      }
    }
    catch (Exception e)
    {
      Console.WriteLine("Utils.checksum error: " + e.Message);

      throw;
    }

    var selection = long.Parse(result[..8], System.Globalization.NumberStyles.HexNumber);

    return selection;
  }
}