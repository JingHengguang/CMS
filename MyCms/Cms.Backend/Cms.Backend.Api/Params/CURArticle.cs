using System.Collections.Generic;
using Cms.Backend.Api.Model;

namespace Cms.Backend.Api.Params
{
    public class CURArticle
    {

        
        //标题
        public string Title { get; set; }

        //简介
        public string Synopsis { get; set; }

        //浏览量
        public int ReadingsNum { get; set; }
        //点赞量
        public int LikesNum { get; set; }

        //是否推荐
        public bool IsRecommend { get; set; }

        //备注
        public string Remarks { get; set; }
      
        //  内容id
        public int ContentId { get; set; }

        //用户id
        public int UserId { get; set; }
    
        // 类型
        public int TypeId { get; set; }

        //用户
        public string User { get; set; }
    
        //封面图路径
        public string coverPictureUrl { get; set; }
        
        //发布的内容,这里图片可以为空
        public string Words { get; set; }
        public string Picture { get; set; }
        public string Video { get; set; }

    }
}