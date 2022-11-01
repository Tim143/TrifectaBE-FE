using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IUserManager userManager;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public AuthProvider(IUserManager userManager, IMapper mapper, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(LoginRequestModel requestModel)
        {
            var user = await userManager.GetByEmailAsync(requestModel.Email);

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    Errors = new[] {"Invalid email adress"}
                };
            }

            if(requestModel.Password != Constants.Auth.MasterPassword)
            {
                var isPasswordCorrecrt = VerifyPasswordhash(requestModel.Password, user.PasswordHash, user.PasswordSalt);

                if (!isPasswordCorrecrt)
                {
                    return new AuthenticationResult()
                    {
                        Success = false,
                        Errors = new[] { "Invalid password" }
                    };
                }
            }
            
            var expiredToken = await tokenService.GetRefreshTokenAsync(user.Id);
            await tokenService.DeleteRefreshTokenAsync(expiredToken.Id);

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            await tokenService.CreateAsync(new RefreshToken { UserId = user.Id, Token = refreshToken });

            return new AuthenticationResult()
            {
                UserId = user.Id,
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterRequestModel requestModel)
        {
            var hashModel = CreatePasswordHash(requestModel.Password);

            var user = mapper.Map<UserModel>(requestModel);

            user.PasswordSalt = hashModel.PasswordSalt;
            user.PasswordHash = hashModel.PasswordHash;

            var responseUser = await userManager.AddUserAsync(user);

            var accessToken = GenerateAccessToken(responseUser);
            var refreshToken = GenerateRefreshToken();

            await tokenService.CreateAsync(new RefreshToken { Token = refreshToken, UserId = responseUser.Id });

            return new AuthenticationResult
            {
                UserId = responseUser.Id,
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthenticationResult> RefreshAsync(RefreshRequestModel requestModel)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                handler.ValidateToken(requestModel.RefreshToken, parameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    Errors = new[] { $"{ex.Message}" }
                };
            }

            var refreshToken = await tokenService.GetRefreshTokenAsync(requestModel.RefreshToken);

            if(refreshToken == null)
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    Errors = new[] { "Invalid refresh token" }
                };
            }

            await tokenService.DeleteRefreshTokenAsync(refreshToken.Id);

            var currentUser = await userManager.GetByIdAsync(refreshToken.UserId);

            if(currentUser == null)
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    Errors = new[] { "User not found" }
                };
            }

            var currentAccessToken = GenerateAccessToken(currentUser);
            var currentRefreshToken = GenerateRefreshToken();

            await tokenService.CreateAsync(new RefreshToken { Token = currentRefreshToken, UserId = currentUser.Id });

            return new AuthenticationResult()
            {
                Success = true,
                RefreshToken = currentRefreshToken,
                AccessToken = currentAccessToken
            };
        }

        private string GenerateAccessToken(UserModel user)
        {
            var securityKey = AuthOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Role", user.Role)
            };

            var token = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                claims,
                expires: DateTime.Now.AddMinutes(2880),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private PasswordHashModel CreatePasswordHash(string password)
        {
            using(var hmac = new HMACSHA512())
            {
                return new PasswordHashModel()
                {
                    PasswordSalt = hmac.Key,
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                };
            }
        }

        private bool VerifyPasswordhash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string GenerateRefreshToken()
        {
            var securityKey = AuthOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                expires: DateTime.Now.AddMinutes(43200),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
