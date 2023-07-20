using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cms.Backend.Api.Model
{
    public class Users : BaseModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<Articles> Article { get; set; }

  
        public virtual UserRoles UserRole { get; set; }

        public int UserRoleId { get; set; }
    }
}