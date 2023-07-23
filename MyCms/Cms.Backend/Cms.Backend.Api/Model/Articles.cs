using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cms.Backend.Api.Model
{

    /// <summary>
    /// 文章表
    /// </summary>
    public class Articles : BaseModel
    {
        [Required]
        public string Title { get; set; }
        //简要
        public string Synopsis { get; set; }
        public int ReadingsNum { get; set; }
        public int LikesNum { get; set; }

        //封面图
        public string CoverPictureUrl { get; set; }


        /// 是否审核   0.待审核 1.已审核 2.已退回
        [DefaultValue(0)]
        public int IsCheck { get; set; }

        //是否推荐
        [DefaultValue(false)]
        public bool IsRecommend { get; set; }



        public virtual Contents Contents { get; set; }
        public int ContentsId { get; set; }

        public virtual Types Type { get; set; }
        public int TypeId { get; set; }

        public virtual Users User { get; set; }
        public int UserId { get; set; }

        //评论表外键
        public IEnumerable<Comments> Comments { get; set; }

        public IEnumerable<Likes> Likes { get; set; }

    }
}