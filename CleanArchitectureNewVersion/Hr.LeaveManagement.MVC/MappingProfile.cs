using AutoMapper;
using Hr.LeaveManagement.Application.Models.Identity;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Models.LeaveAllocation;
using Hr.LeaveManagement.MVC.Models.LeaveRequest;
using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto,CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<RegisterVM, Hr.LeaveManagement.MVC.Services.Base.RegistrationRequest>().ReverseMap();
            CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestVM>()
               .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
               .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
               .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
               .ReverseMap();
            CreateMap<EmployeeVM, Hr.LeaveManagement.MVC.Services.Base.Employee>().ReverseMap();
        }
    }
}
