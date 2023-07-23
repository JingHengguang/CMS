using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cms.Backend.Api.Model
{
    /// <summary>
    /// 文章内容表
    /// </summary>
    public class Contents : BaseModel
    {      
        [Required]
        public string Words { get; set; }
        public string Picture { get; set; }
        public string Video { get; set; }

        //文章表的外键
        public IEnumerable<Articles> Article { get; set; }
    }
}