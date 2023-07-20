using System.Collections.Generic;
namespace Cms.Backend.Api.Model
{
    public class Comments : BaseModel
    {
        public string Comment { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public virtual Articles Article { get; set; }
        public int ArticleId { get; set; }
    }
}