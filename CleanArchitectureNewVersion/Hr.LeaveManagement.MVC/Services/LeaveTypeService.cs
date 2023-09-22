using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper mapper;
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public LeaveTypeService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService)
           : base(httpClient, localStorageService)
        {
            this.mapper = mapper;
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }
        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveTypeDto createlLeaveType = this.mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerToken();
                var apiResponse = await this.httpClient.LeaveTypePOSTAsync(createlLeaveType);
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

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                AddBearerToken();
                await this.httpClient.LeaveTypeDELETEAsync(id);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            AddBearerToken();
            var leaveType = await this.httpClient.LeaveTypeGETAsync(id);
            return this.mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            AddBearerToken();
            var leaveTypes = await this.httpClient.LeaveTypeAllAsync();
            return this.mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(LeaveTypeVM leaveType)
        {
            try
            {
                LeaveTypeDto leavetypeDto = this.mapper.Map<LeaveTypeDto>(leaveType);
                AddBearerToken();
                await this.httpClient.LeaveTypePUTAsync(leavetypeDto);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
