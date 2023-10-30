using Microsoft.AspNetCore.Mvc.Filters;

namespace Kanban.API.Filters
{
  public class ExceptionsLogger : ExceptionFilterAttribute
  {
    private readonly ILogger<ExceptionsLogger> _logger;
    string folderPath = @"E:\Logs\Kanban_API";
    private string filePath = @"E:\Logs\Kanban_API\error.txt";
    public ExceptionsLogger(ILogger<ExceptionsLogger> logger)
      => _logger = logger;

    public override void OnException(ExceptionContext context)
    {
      if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath);

      using (StreamWriter sw = File.CreateText(filePath))
        sw.WriteLine(context.Exception.Message);

      _logger.LogError(context.Exception, context.Exception.Message);
      base.OnException(context);
    }
  }
}
