using System;
using DogSkill.Api.Data.Entities;

namespace DogSkill.Api.Communications
{
    public class UserActivityGetResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }

        public UserActivityGetResponse()
        {
        }

        public UserActivityGetResponse(UserActivity entity)
        {
            UserId = entity.UserId;
            UserName = entity.User.UserName;
            Score = entity.Score;
        }
    }
}
