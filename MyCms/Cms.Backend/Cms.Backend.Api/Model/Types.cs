using System.Collections.Generic;

namespace Cms.Backend.Api.Model
{
    public class Types : BaseModel
    {
        public string Type { get; set; }
        
        //文章表外键
        public IEnumerable<Articles> Article { get; set; }
    }
}