namespace  Cms.Backend.Api.Model
{
    public class Likes : BaseModel
    {
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public bool applaud { get; set; }
       
    }
}