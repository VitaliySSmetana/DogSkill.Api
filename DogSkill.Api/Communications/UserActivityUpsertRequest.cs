using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogSkill.Api.Communications
{
    public class UserActivityUpsertRequest
    {
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }
}
