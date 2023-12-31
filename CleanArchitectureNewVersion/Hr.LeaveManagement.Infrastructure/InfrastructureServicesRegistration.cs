﻿using Hr.LeaveManagement.Application.Contracts.Infrastructure;
using Hr.LeaveManagement.Application.Models;
using Hr.LeaveManagement.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hr.LeaveManagement.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
