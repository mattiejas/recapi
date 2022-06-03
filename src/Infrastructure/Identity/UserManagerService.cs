using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Recapi.Application.Common.Interfaces;
using Recapi.Application.Common.Models;

namespace Recapi.Infrastructure.Identity
{
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserManagerService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<(Result Result, string UserId)> ValidateCredentialsAsync(string userName, string password)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return (Result.Failure(new[] {"User does not exist"}), null);
            }

            var isValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isValid)
            {
                return (Result.Failure(new[] {"Invalid credentials"}), null);
            }

            return (Result.Success(), user.Id);
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public Task<(Result Result, string Token)> GenerateTokenAsync(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Task.FromResult((Result.Success(), tokenString));
        }


        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}