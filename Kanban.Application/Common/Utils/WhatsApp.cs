﻿using Kanban.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Kanban.Application.Common.Utils
{
  public static class WhatsApp
  {
    private static readonly string _token;
    private static readonly string _endPoint;

    static WhatsApp()
    {
      var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

      _token = configuration["WhatsAppSettings:Token"]!;
      var idPhone = configuration["WhatsAppSettings:IdPhone"]!;
      _endPoint = string.Format(_endPoint, idPhone);
    }

    public async static Task SendMessage (string message, string recipient, string template)
    {
      var payload = new
      {
        messaging_product = "whatsapp",
        to = recipient,
        type = "template",
        template = new
        {
          name = template,
          language = new { code = "es_MX" },
          components = new[]
          {
              new
              {
                  type = "body",
                  parameters = new[]
                  {
                      new { type = "text", text = message }
                  }
              }
            }
        },
      };
      var jsonPayload = JsonConvert.SerializeObject(payload);

      HttpClient client = new();
      HttpRequestMessage request = new(HttpMethod.Post, _endPoint);
      request.Headers.Add("Authorization", $"Bearer {_token}");
      request.Content = new StringContent(jsonPayload);
      request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      HttpResponseMessage responseMessage = await client.SendAsync(request);
      await responseMessage.Content.ReadAsStringAsync();
    }
  }
}
