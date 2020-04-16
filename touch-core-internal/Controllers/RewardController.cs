using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Dtos.Reward;
using touch_core_internal.Services;
using touch_core_internal.Services.RewardService;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/reward")]
    public class RewardController : ControllerBase
    {
        public RewardController(IRewardService rewardService)
        {
            this.RewardService = rewardService;
        }

        public IRewardService RewardService { get; set; }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.RewardService.GetAllRewardsAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetRewardDto>();
            if (id.HasValue)
            {
                serviceResponse = await this.RewardService.GetRewardByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Reward does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Reward exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Reward does not exist", false);
                return this.NotFound(serviceResponse);
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertReward(AddRewardDto newReward)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.RewardService.AddNewRewardAsync(newReward);
            return this.Ok(serviceResponse);
        }
    }
}