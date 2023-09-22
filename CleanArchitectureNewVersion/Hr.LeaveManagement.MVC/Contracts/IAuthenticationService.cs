namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(string firstName, string LastName, string userName, string email, string password);
        Task Logout();
    }
}
