using BC = BCrypt.Net.BCrypt;

namespace Kanban.Application.Common.Utils
{
  public static class Encrypt
  {
    public static string EncriptText(string text)
      => BC.HashPassword(text);

    public static bool MatchText(string text, string hashed)
      => BC.Verify(text, hashed);
  }
}
