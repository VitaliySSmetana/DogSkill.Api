using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Communications;
using DogSkill.Api.Data.Entities;

namespace DogSkill.Api.Services.Interfaces
{
    public interface IUserActivityService
    {
        Task<List<UserActivityGetResponse>> GetTopTenAsync();
        Task UpsertActivityAsync(UserActivityUpsertRequest request);
    }
}
