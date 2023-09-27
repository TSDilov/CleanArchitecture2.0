using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IClient httpclient;

        public LeaveAllocationService(IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            this.localStorageService = localStorageService;
            this.httpclient = httpclient;
        }
        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };
                AddBearerToken();
                var apiResponse = await this.httpClient.LeaveAllocationPOSTAsync(createLeaveAllocation);
                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
