using System.Threading.Tasks;
using Recapi.Application.Common.Models;

namespace Recapi.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<(Result Result, string UserId)> ValidateCredentialsAsync(string userId, string password);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId); 
        Task<(Result Result, string Token)> GenerateTokenAsync(string userId);
    }
}
