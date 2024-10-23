namespace backend.Utils
{
  public static class EnvVariables
  {
    public static readonly string HOST;
    public static readonly string PORT;
    public static readonly string CLIENT_URL;
    public static readonly string SUPABASE_URI;

    static EnvVariables()
    {
      HOST = GetValue("HOST", "Host");
      PORT = GetValue("PORT", "Port");
      CLIENT_URL = GetValue("CLIENT_URL", "Client URL");
      SUPABASE_URI = GetValue("SUPABASE_URI", "Database URI");
    }

    private static string GetValue(string parameter, string text)
    {
      string? value = Environment.GetEnvironmentVariable(parameter);
      if (string.IsNullOrEmpty(value))
        throw new InvalidOperationException($"{text} not set");

      return value;
    }
  }
}