using System.Threading.Tasks;
using Recapi.Application.Common.Interfaces;
using Recapi.Application.Notifications.Models;

namespace Recapi.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}
