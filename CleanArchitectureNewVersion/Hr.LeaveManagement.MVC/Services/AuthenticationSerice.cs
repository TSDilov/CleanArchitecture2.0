using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = Hr.LeaveManagement.MVC.Contracts.IAuthenticationService;

namespace Hr.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private JwtSecurityTokenHandler tokenHandler;

        public AuthenticationService(IClient httpClient, 
            ILocalStorageService localStorageService, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(httpClient, localStorageService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authRequest = new() { Email = email, Password = password };
                var authResponse = await this.httpClient.LoginAsync(authRequest);
                if (authResponse.Token != string.Empty) 
                {
                    var tokenContent = this.tokenHandler.ReadJwtToken(authResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = this.httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    this.localStorageService.SetStorageValue("token", authResponse.Token);

                    return true;
                }

                return false; 
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Register(RegisterVM registration)
        {
            try
            {
                RegistrationRequest registrationRequest = this.mapper.Map<RegistrationRequest>(registration);

                var response = await this.httpClient.RegisterAsync(registrationRequest);
                if (!string.IsNullOrEmpty(response.UserId))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Logout()
        {
            this.localStorageService.Clearstorage(new List<string> { "token" });
            await this.httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
