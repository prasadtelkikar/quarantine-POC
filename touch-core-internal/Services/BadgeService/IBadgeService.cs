using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Badge;

namespace touch_core_internal.Services.BadgeService
{
    public interface IBadgeService
    {
        Task<ServiceResponse<List<GetBadgeDto>>> AddNewBadgeAsync(AddBadgeDto newBadge);

        Task<ServiceResponse<List<GetBadgeDto>>> DeleteBadgeAsync(Guid id);

        Task<ServiceResponse<List<GetBadgeDto>>> GetAllBadgesAsync();

        Task<ServiceResponse<GetBadgeDto>> GetBadgeByIdAsync(Guid id);
    }
}