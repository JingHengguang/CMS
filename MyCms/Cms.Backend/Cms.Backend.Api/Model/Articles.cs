using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cms.Backend.Api.Model
{
    public class Articles : BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public int ReadingsNum { get; set; }
        public int LikesNum { get; set; }
        public string CoverPictureUrl { get; set; }

        /// <summary>
        /// 是否审核   0.待审核 1.已审核 2.已退回
        /// </summary>
        /// <value></value>
        [DefaultValue(false)]
        public int IsCheck { get; set; }

        [DefaultValue(false)]
        public bool IsRecommend { get; set; }

        public IEnumerable<Comments> Comments { get; set; }
        public virtual Content Content { get; set; }
        public int ContentId { get; set; }
        public virtual Types Type{ get; set; }
        public int TypeId { get; set; }
        public virtual Users User { get; set; }
        public int UserId { get; set; }

    }
}