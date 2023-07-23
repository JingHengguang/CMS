using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cms.Backend.Api.Model
{
    /// <summary>
    /// 评论表
    /// </summary>
    /// <value></value>
    public class Comments : BaseModel
    {

        /// <summary>
        /// 评价用户
        /// </summary>
        /// <value></value>
        // public virtual Users FromUser { get; set; }
        public int FromUserId { get; set; }

        /// <summary>
        /// 被评论的文章
        /// </summary>
        /// <value></value>
        public virtual Articles Articles { get; set; }
        [Required]
        public int ArticlesId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}