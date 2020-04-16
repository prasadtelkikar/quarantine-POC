using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.TimeSheet;

namespace touch_core_internal.Services.TimesheetService
{
    public interface ITimeSheetService
    {
        Task<ServiceResponse<List<GetTimeSheetDto>>> AddNewTimeSheetAsync(AddTimeSheetDto newTimeSheet);

        Task<ServiceResponse<List<GetTimeSheetDto>>> DeleteTimeSheetAsync(Guid id);

        Task<ServiceResponse<List<GetTimeSheetDto>>> GetAllTimesheetsAsync();

        Task<ServiceResponse<GetTimeSheetDto>> GetTimeSheetByIdAsync(Guid id);
    }
}