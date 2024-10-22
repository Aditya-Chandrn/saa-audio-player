namespace backend.Utils
{
  public abstract class PrintLog
  {
    public PrintLog(int type, string Message, Exception? Exception = null)
    {
      DateTime time = DateTime.Now;

      string timeString = $"{time.Hour}:{time.Minute}:{time.Second}.{time.Millisecond} T {time.Day:D2}-{time.Month:D2}-{time.Year}";
      if (type == 1)
        Console.WriteLine($"✅ {Message} \t\t [{timeString}]");

      else if (type == 0)
        Console.WriteLine($"⚠️ {Message} \t\t [{timeString}]");

      else if (type == -1)
      {
        Console.WriteLine($"❌ {Message} \t\t [{timeString}]");
        Console.WriteLine(Exception);
      }
    }
  }

  public class PrintSuccess(string message) : PrintLog(1, message) { }
  public class PrintFailure(string message) : PrintLog(0, message) { }
  public class PrintError(string message, Exception exception) : PrintLog(-1, message, exception) { }
}