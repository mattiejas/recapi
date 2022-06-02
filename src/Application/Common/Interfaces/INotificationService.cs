using System.Threading.Tasks;
using Recapi.Application.Notifications.Models;

namespace Recapi.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(MessageDto message);
    }
}
