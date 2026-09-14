using AutoAlertBackEnd.Models;

namespace AutoAlertBackEnd.NotificationDelivery;

public sealed class NotificationDeliveryService : INotificationDeliveryService
{
    private readonly IEmailNotificationSender _emailSender;

    public NotificationDeliveryService(IEmailNotificationSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task<NotificationDeliveryResult> DeliverAsync(Notifications notification, Users recipient, CancellationToken cancellationToken = default)
    {
        return notification.Channel switch
        {
            "Email" => _emailSender.SendAsync(notification, recipient, cancellationToken),
            "WhatsApp" or "SMS" => Task.FromResult(new NotificationDeliveryResult(false, $"Pendiente: canal {notification.Channel} no configurado")),
            _ => Task.FromResult(new NotificationDeliveryResult(false, "Fallido: canal no válido")),
        };
    }
}
