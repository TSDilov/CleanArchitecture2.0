﻿using Hr.LeaveManagement.Application.Constants;
using Hr.LeaveManagement.Application.Contracts.Identity;
using Hr.LeaveManagement.Application.Models.Identity;
using Hr.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hr.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOptionsMonitor<JwtSettings> jwtSettings;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptionsMonitor<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
            this.signInManager = signInManager;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await this.userManager.FindByEmailAsync(request.Email);
            if (user == null) 
            {
                throw new Exception($"User with {request.Email} not found.");
            }

            var result = await this.signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) 
            {
                throw new Exception($"Credentials for {request.Email} aren't valid");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await this.userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"Username {request.UserName} already exists");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await this.userManager.FindByEmailAsync(request.Email);
            if (existingEmail == null) 
            {
                var result = await this.userManager.CreateAsync(user, request.Password);
                if (result.Succeeded) 
                {
                    await this.userManager.AddToRoleAsync(user, "Employee");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email {request.Email} already exists.");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await this.userManager.GetClaimsAsync(user);
            var roles = await this.userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.CurrentValue.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this.jwtSettings.CurrentValue.Issuer,
                audience: this.jwtSettings.CurrentValue.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(this.jwtSettings.CurrentValue.DurationInMinutes),
                signingCredentials: signInCredentials);

            return jwtSecurityToken;
        }
    }
}
