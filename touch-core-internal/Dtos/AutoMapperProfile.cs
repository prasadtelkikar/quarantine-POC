using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using touch_core_internal.Dtos.Badge;
using touch_core_internal.Dtos.Category;
using touch_core_internal.Dtos.Employee;
using touch_core_internal.Dtos.Reward;
using touch_core_internal.Dtos.TimeSheet;

namespace touch_core_internal.Dtos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Employee, GetEmployeeDto>();
            CreateMap<Models.Employee, GetTimeSheetToEmployeeDto>();
            CreateMap<AddEmployeeDto, Models.Employee>();
            CreateMap<Models.TimeSheet, GetEmployeeToTimesheetDto>();

            CreateMap<Models.TimeSheet, GetTimeSheetDto>();
            CreateMap<AddTimeSheetDto, Models.TimeSheet>();

            CreateMap<Models.Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Models.Category>();

            CreateMap<Models.Badge, GetBadgeDto>();
            CreateMap<AddBadgeDto, Models.Badge>();

            CreateMap<Models.Reward, GetRewardDto>();
            CreateMap<AddRewardDto, Models.Reward>();
        }
    }
}