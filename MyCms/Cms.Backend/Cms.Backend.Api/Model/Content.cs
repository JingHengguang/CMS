using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cms.Backend.Api.Model
{
    public class Content : BaseModel
    {
        [Required]
        public string Words { get; set; }
        public string Picture { get; set; }
        public string Video { get; set; }
        public IEnumerable<Articles> Article { get; set; }
    }
}