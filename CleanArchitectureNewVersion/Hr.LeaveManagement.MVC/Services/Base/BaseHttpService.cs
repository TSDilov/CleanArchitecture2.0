using Hr.LeaveManagement.MVC.Contracts;
using System.Net.Http.Headers;

namespace Hr.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly IClient httpClient;
        protected readonly ILocalStorageService localStorageService;

        public BaseHttpService(IClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>()
                {
                    Message = "Validation errors have occured",
                    ValidationErrors = ex.Response,
                    Success = false
                };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>()
                {
                    Message = "The requested item could not be found",
                    Success = false
                };
            }
            else
            {
                return new Response<Guid>()
                {
                    Message = "Something went wrong, please try again",
                    Success = false
                };
            }
        }

        protected void AddBearerToken()
        {
            if (this.localStorageService.Exists("token"))
            {
                this.httpClient.HttpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", this.localStorageService.GetStorageValue<string>("token"));
            }
        }
    }
}
