using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Kanban.Application.Common.Utils
{
  public class EmailSender
  {
    private readonly string _senderEmail;
    private readonly string _senderPassword;
    private readonly int _port;
    private readonly string _server;

    public EmailSender(IConfiguration configuration)
    {
      _senderEmail = configuration["Smtp:SenderEmail"]!;
      _senderPassword = configuration["Smtp:SenderPassword"]!;
      _port = int.Parse(configuration["Smtp:Port"]!);
      _server = configuration["Smtp:Server"]!;
    }

    public void SendEmail(string to, string subject, string body, bool isBodyHtml = true)
    {
      SmtpClient client = new(_server)
      {
        Port = _port,
        Credentials = new NetworkCredential(_senderEmail, _senderPassword),
        EnableSsl = false, // Cambia a true si tu servidor SMTP requiere SSL/TLS
      };

      MailMessage mail = new(_senderEmail, to)
      {
        Subject = subject,
        Body = body,
        IsBodyHtml = isBodyHtml,
      };

      client.Send(mail);
    }
  }
}
