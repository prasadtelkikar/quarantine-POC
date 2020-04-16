using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Reward;

namespace touch_core_internal.Services.RewardService
{
    public interface IRewardService
    {
        Task<ServiceResponse<Guid>> AddNewRewardAsync(AddRewardDto newReward);

        Task<ServiceResponse<List<GetRewardDto>>> GetAllRewardsAsync();

        Task<ServiceResponse<GetRewardDto>> GetRewardByIdAsync(Guid id);
    }
}