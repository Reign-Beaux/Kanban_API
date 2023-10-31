using Microsoft.Extensions.Logging;

namespace Kanban.Application.Utils
{
  public class ExceptionsLogger
  {
    private readonly ILogger<ExceptionsLogger> _logger;
    string folderPath = @"E:\Logs\Kanban_API";
    private string filePath = @$"E:\Logs\Kanban_API\{DateTime.Now:yyyy_MM_dd}.txt";

    public ExceptionsLogger()
    {
      if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath);
    }


    public void SetException(string message)
    {

      if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath);

      using (StreamWriter sw = File.AppendText(filePath))
        sw.WriteLine($"{DateTime.Now:HH:mm:ss} - {message}");
    }
  }
}
