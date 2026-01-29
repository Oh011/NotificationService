using NotificationService.Application.Dtos.Authentication.Responses;
using NotificationService.Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Abstractions.Security
{
    public interface ITokenService
    {


        public  Task<Result<Unit>> RevokeRefreshTokenByToken(string token, string DeviceId);

        public Task<Result<LogInUserResponse>> RefreshAccessTokenAsync(string refreshToken, string DeviceId);
        public Task<string> CreateRefreshTokenForDevice(string userId, string DeviceId);
        public string GenerateAccessToken(string userId, string Email, string UserName , IList<string> roles);
    }
}
