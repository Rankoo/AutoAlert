using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AutoAlertBackEnd.Models;
using Microsoft.Extensions.Options;

namespace AutoAlertBackEnd.NotificationDelivery;

public sealed class SendGridEmailNotificationSender : IEmailNotificationSender
{
    private readonly HttpClient _httpClient;
    private readonly SendGridOptions _options;
    private readonly ILogger<SendGridEmailNotificationSender> _logger;

    public SendGridEmailNotificationSender(HttpClient httpClient, IOptions<SendGridOptions> options, ILogger<SendGridEmailNotificationSender> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<NotificationDeliveryResult> SendAsync(Notifications notification, Users recipient, CancellationToken cancellationToken = default)
    {
        if (!_options.IsConfigured)
            return new(false, "Pendiente: configura SendGrid");

        if (string.IsNullOrWhiteSpace(recipient.Email))
            return new(false, "Fallido: usuario sin email");

        var payload = new
        {
            from = new { email = _options.SenderEmail, name = _options.SenderName },
            personalizations = new[] { new { to = new[] { new { email = recipient.Email, name = $"{recipient.Names} {recipient.LastNames}".Trim() } } } },
            subject = notification.Title,
            content = new[]
            {
                new { type = "text/plain", value = notification.Message },
                new { type = "text/html", value = $"<p>{WebUtility.HtmlEncode(notification.Message)}</p>" },
            },
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "v3/mail/send")
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"),
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiKey);

        try
        {
            using var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
                return new(true, "Enviado", DateTime.UtcNow);

            _logger.LogWarning("SendGrid rechazó la notificación {NotificationId} con estado {StatusCode}", notification.Id, (int)response.StatusCode);
            return new(false, $"Fallido: SendGrid {(int)response.StatusCode}");
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "No se pudo enviar la notificación {NotificationId} mediante SendGrid", notification.Id);
            return new(false, "Fallido: proveedor no disponible");
        }
    }
}
