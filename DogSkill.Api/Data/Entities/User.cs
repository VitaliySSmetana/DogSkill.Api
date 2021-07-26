using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DogSkill.Api.Data.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
