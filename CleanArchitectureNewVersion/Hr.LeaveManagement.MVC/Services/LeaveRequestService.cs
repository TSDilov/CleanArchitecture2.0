using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models.LeaveAllocation;
using Hr.LeaveManagement.MVC.Models.LeaveRequest;
using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IMapper mapper;
        private readonly IClient httpclient;

        public LeaveRequestService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService) : base(httpclient, localStorageService)
        {
            this.localStorageService = localStorageService;
            this.mapper = mapper;
            this.httpclient = httpclient;
        }
        public async Task ApproveLeaveRequest(int id, bool approved)
        {
            AddBearerToken();
            try
            {
                var request = new ChangeLeaveRequestApprovalDto { Approved = approved, Id = id };
                await this.httpClient.ChangeapprovalAsync(id, request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto createLeaveRequest = this.mapper.Map<CreateLeaveRequestDto>(leaveRequest);
                AddBearerToken();
                var apiResponse = await this.httpclient.LeaveRequestPOSTAsync(createLeaveRequest);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
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

        public Task DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            AddBearerToken();
            var leaveRequests = await this.httpclient.LeaveRequestAllAsync();

            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = this.mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };
            return model;
        }

        public async Task<LeaveRequestVM> GetLeaveRequest(int id)
        {
            AddBearerToken();
            var leaveRequest = await this.httpclient.LeaveRequestGETAsync(id);
            return this.mapper.Map<LeaveRequestVM>(leaveRequest);
        }

        public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
        {
            AddBearerToken();
            var leaveRequests = await this.httpclient.LeaveRequestAllAsync();
            var allocations = await this.httpclient.LeaveAllocationAllAsync();
            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = this.mapper.Map<List<LeaveAllocationVM>>(allocations),
                LeaveRequests = this.mapper.Map<List<LeaveRequestVM>>(leaveRequests)
            };

            return model;
        }
    }
}
