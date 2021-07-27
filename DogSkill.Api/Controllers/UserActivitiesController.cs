using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Communications;
using DogSkill.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DogSkill.Api.Controllers
{
    [Route("activities")]
    public class UserActivitiesController : ControllerBase
    {
        private readonly IUserActivityService _activityService;

        public UserActivitiesController(IUserActivityService activityService)
        {
            _activityService = activityService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<UserActivityGetResponse>> GetTopAsync()
        {
            return await _activityService.GetTopTenAsync();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpsertUserActivityAsync([FromBody] UserActivityUpsertRequest request)
        {
            await _activityService.UpsertActivityAsync(request);
            return Ok();
        }
    }
}
