using Hr.LeaveManagement.Application.Models;

namespace Hr.LeaveManagement.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
