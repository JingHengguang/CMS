using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cms.Backend.Api.Model
{
    public class UserRoles : BaseModel
    {
        [Required]
        public string  RoleName { get; set; }
        
       
        public IEnumerable<Users> Users { get; set; }
    }
}