namespace Cms.Backend.Api.Params
{
    public class CURComment
    {
       public string Comment { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int ArticleId { get; set; }

    }
}