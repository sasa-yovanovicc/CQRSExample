using AutoMapper;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Models.Constants;

namespace EmployeeTimeTracking.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// AutoMapper profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeResponseModel>()
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => src.TotalHours));
            CreateMap<EmployeeRequestModel, Employee>()
                .ForMember(dest => dest.TotalHours, opt => opt.Ignore())
                .ForMember(dest => dest.WorkIntervals, opt => opt.Ignore());
            CreateMap<EmployeeResponseModel, WorkIntervalRequestModel>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Start, opt => opt.Ignore())
                .ForMember(dest => dest.End, opt => opt.Ignore());
            CreateMap<WorkIntervalRequestModel, WorkInterval>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Start, opt =>
                    opt.MapFrom(src => src.Start.AddTicks(-(src.Start.Ticks % TimeSpan.TicksPerMinute))))
                .ForMember(dest => dest.End, opt =>
                    opt.MapFrom(src => src.End.AddTicks(-(src.End.Ticks % TimeSpan.TicksPerMinute))));
            CreateMap<WorkIntervalSaveRequestModel, WorkInterval>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start == DateConstants.MinDate ? DateConstants.SpecialDate : src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End == DateConstants.MinDate ? DateConstants.SpecialDate : src.End));
            CreateMap<WorkInterval, WorkIntervalResponseModel>()
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.Position, opt => opt.Ignore())
                .ForMember(dest => dest.Duration, opt => opt.Ignore());
        }
    }

}

