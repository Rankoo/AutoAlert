using AutoAlertBackEnd.Context;
using AutoAlertBackEnd.Models;
using AutoAlertBackEnd.NotificationDelivery;
using Microsoft.EntityFrameworkCore;

namespace AutoAlertBackEnd.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly AutoAlertContext _context;
    private readonly INotificationDeliveryQueue _notificationDeliveryQueue;

    public AlertRepository(AutoAlertContext context, INotificationDeliveryQueue notificationDeliveryQueue)
    {
        _context = context;
        _notificationDeliveryQueue = notificationDeliveryQueue;
    }

    public async Task<IEnumerable<Alerts>> GetAllAsync()
    {
        return await _context.Alerts.ToListAsync();
    }

    public async Task<IEnumerable<Alerts>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Alerts
            .Where(alert => _context.Notifications.Any(notification =>
                notification.AlertId == alert.Id && notification.UserId == userId))
            .ToListAsync();
    }

    public async Task<Alerts?> GetByIdAsync(Guid id)
    {
        return await _context.Alerts.FindAsync(id);
    }

    public async Task<IEnumerable<Alerts>> GetByServiceIdAsync(Guid serviceId)
    {
        return await _context.Alerts
            .Where(a => a.ServiceId == serviceId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Alerts>> GetScheduledAlertsAsync(DateTime? fromDate = null)
    {
        var query = _context.Alerts.AsQueryable();
        
        if (fromDate.HasValue)
        {
            query = query.Where(a => a.DueDate >= fromDate.Value);
        }
        
        return await query.OrderBy(a => a.DueDate).ToListAsync();
    }

    public async Task<Alerts> CreateAsync(Alerts alert)
    {
        var service = await _context.Services
            .Include(s => s.Store)
            .FirstOrDefaultAsync(s => s.Id == alert.ServiceId);
        if (service == null)
            throw new InvalidOperationException("El servicio seleccionado no existe.");

        alert.Status = string.IsNullOrWhiteSpace(alert.Status) ? "Pendiente" : alert.Status;
        _context.Alerts.Add(alert);
        await _context.SaveChangesAsync();

        var recipients = await _context.Users.Where(u => u.IsActive).ToListAsync();
        var dueDate = alert.DueDate.ToString("dd/MM/yyyy");
        var notifications = new List<Notifications>();
        foreach (var recipient in recipients)
        {
            notifications.Add(new Notifications
            {
                AlertId = alert.Id,
                UserId = recipient.Id,
                Title = "Pago próximo a vencer",
                Message = $"{service.Name} de {service.Store?.Name ?? "la tienda"} vence el {dueDate} por {alert.Amount:C0}.",
                Channel = "Email",
                Result = "Pendiente"
            });
        }
        _context.Notifications.AddRange(notifications);
        await _context.SaveChangesAsync();

        foreach (var notification in notifications)
            await _notificationDeliveryQueue.EnqueueAsync(notification.Id);
        return alert;
    }

    public async Task<Alerts?> UpdateAsync(Alerts alert)
    {
        var existing = await _context.Alerts.FindAsync(alert.Id);
        if (existing == null) return null;
        _context.Entry(existing).CurrentValues.SetValues(alert);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Alerts?> UpdateStatusAsync(Guid id, string status)
    {
        var existing = await _context.Alerts.FindAsync(id);
        if (existing is null) return null;

        existing.Status = status;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Alerts.FindAsync(id);
        if (existing == null) return false;

        var notifications = await _context.Notifications
            .Where(notification => notification.AlertId == id)
            .ToListAsync();

        _context.Notifications.RemoveRange(notifications);
        _context.Alerts.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}

