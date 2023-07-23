namespace  Cms.Backend.Api.Model
{
    public class Likes : BaseModel
    {
        /// <summary>
        /// 点赞用户
        /// </summary>
        /// <value></value>
        public virtual Users FromUser { get; set; }
        public int FromUserId { get; set; }

        /// <summary>
        /// 点赞文章
        /// </summary>
        /// <value></value>
        public virtual Articles Articles { get; set; }
        public int ArticleId { get; set; }
        
        /// <summary>
        /// 是否点赞
        /// </summary>
        /// <value></value>
        public bool IsPraise { get; set; }

       
    }
}