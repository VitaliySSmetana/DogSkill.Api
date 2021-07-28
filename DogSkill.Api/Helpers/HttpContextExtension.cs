using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace DogSkill.Api.Helpers
{
    public static class HttpContextExtension
    {
        public static User GetUserData(this HttpContext context)
        {
            return (User) context.Items["User"];
        }
    }
}
