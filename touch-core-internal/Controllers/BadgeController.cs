using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Dtos.Badge;
using touch_core_internal.Services;
using touch_core_internal.Services.BadgeService;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/badge")]
    public class BadgeController : ControllerBase
    {
        public BadgeController(IBadgeService badgeService)
        {
            this.BadgeService = badgeService;
        }

        public IBadgeService BadgeService { get; set; }

        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.BadgeService.DeleteBadgeAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Badge not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.BadgeService.GetAllBadgesAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetBadgeDto>();
            if (id.HasValue)
            {
                serviceResponse = await this.BadgeService.GetBadgeByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Badge does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Badge exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Badge does not exist", false);
                return this.NotFound(serviceResponse);
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertBadge(AddBadgeDto newBadge)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.BadgeService.AddNewBadgeAsync(newBadge);
            return this.Ok(serviceResponse);
        }
    }
}