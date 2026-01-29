using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotificationService.Application.Abstractions.Persistence;
using NotificationService.Application.Abstractions.Security;
using NotificationService.Application.Common.Errors;
using NotificationService.Application.Common.Messages;
using NotificationService.Application.Dtos.Authentication.Requests;
using NotificationService.Application.Dtos.Authentication.Responses;
using NotificationService.Infrastructure.Identity.Models;
using NotificationService.Infrastructure.Persistence.Migrations;
using NotificationService.Shared.Errors;
using NotificationService.Shared.Options;
using NotificationService.Shared.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Identity.Services
{
    internal class TokenService : ITokenService
    {

        private readonly JwtOptions JwtOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;



        public TokenService(IUnitOfWork unitOfWork, IOptions<JwtOptions> jwtOptions,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            JwtOptions = jwtOptions.Value;
            _unitOfWork= unitOfWork;
        }

        public async Task<string> CreateRefreshTokenForDevice(string userId, string DeviceId)
        {

            var repository = _unitOfWork.GetRepository<RefreshToken>();

            var OldToken = (await repository.FindAsync(t => t.UserId == userId && t.DeviceId == DeviceId &&
            !t.IsRevoked && t.ExpiresAt > DateTime.UtcNow)).FirstOrDefault();


            if (OldToken != null)
            {
                 RevokeRefreshTokenAsync(OldToken);
            }

            var NewRefreshToken = new RefreshToken
            {

                UserId = userId,
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(14), // Typically 7 days expiration
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false,
                DeviceId = DeviceId,
            };


            await repository.AddAsync(NewRefreshToken);

            await _unitOfWork.Commit();


            return NewRefreshToken.Token;
        }



        private void  RevokeRefreshTokenAsync(RefreshToken token)
        {
            var repository = _unitOfWork.GetRepository<RefreshToken>();

            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;

            repository.Update(token);
     
        }


        private string GenerateRefreshToken(int size = 64)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(size);

            // Convert to Base64 string
            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateAccessToken(string userId, string Email, string UserName, IList<string> roles)
        {
            
            var JwtOptions= this.JwtOptions;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Email,Email),
                new Claim(ClaimTypes.Name,UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));


            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(JwtOptions.ExpirationInHours),
                Issuer = JwtOptions.Issuer,
                Audience = JwtOptions.Audiance,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token= tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }




        private async Task<Result<RefreshToken>> ValidateRefreshTokenAsync(string token, string deviceId)
        {

            var repository = _unitOfWork.GetRepository<RefreshToken>();

            var existingToken = (await repository.FindAsync(t => t.Token == token && t.DeviceId == deviceId)
                ).FirstOrDefault();


            if (existingToken == null || existingToken.IsExpired || existingToken.IsRevoked)
            {
                return Result<RefreshToken>.Failure(
                    AuthErrorMessages.REFRESH_TOKEN_INVALID_OR_EXPIRED,
                    ErrorType.Unauthorized
                );
            }





            return Result<RefreshToken>.Success(existingToken);
        }

        public async Task<Result<LogInUserResponse>> RefreshAccessTokenAsync(string RefreshToken,string DeviceId)
        {

            var StoredTokenResult = (await ValidateRefreshTokenAsync(RefreshToken, DeviceId));


            if (!StoredTokenResult.IsSuccess)
                return Result<LogInUserResponse>.Failure(StoredTokenResult.Message, StoredTokenResult.Error??ErrorType.Internal);

            var storedToken = StoredTokenResult.Value;
            var user = await _userManager.FindByIdAsync(storedToken.UserId);

            if (user == null)
                return Result<LogInUserResponse>.Failure(AuthErrorMessages.USER_NOT_FOUND, ErrorType.NotFound);


             this.RevokeRefreshTokenAsync(storedToken);

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = GenerateAccessToken(user.UserName, user.Email, user.Id, roles);
            var newRefreshToken = await CreateRefreshTokenForDevice(user.Id,DeviceId);

            var authenticationResponse = new AuthenticationResponse
            (
              user.Email, user.Id, user.UserName, accessToken
            );
            var response = new LogInUserResponse
            (

               authenticationResponse, newRefreshToken
            );


            return Result<LogInUserResponse>.Success(response);
        }



        public async Task<Result<Unit>> RevokeRefreshTokenByToken(string token, string DeviceId)
        {


            var ValidatedRefreshTokenResult = await ValidateRefreshTokenAsync(token, DeviceId);


            if(!ValidatedRefreshTokenResult.IsSuccess)
                return Result<Unit>.FailureUnit(ValidatedRefreshTokenResult.Message, ValidatedRefreshTokenResult.Error ?? ErrorType.Internal);

            var refreshToken = ValidatedRefreshTokenResult.Value;

            RevokeRefreshTokenAsync(refreshToken);


            return Result<Unit>.Success(Unit.Value,AuthSuccessMessages.LOGOUT_SUCCESS);
        }
    }
}
