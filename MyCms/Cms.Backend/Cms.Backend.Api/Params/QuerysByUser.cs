namespace Cms.Backend.Api.Params
{

    public class QuerysByUser
    {  
        /// <summary>
        ///  用户名
        /// </summary>
        /// <value></value>
          public string Username { get; set; }


        /// <summary>
       /// 当前页
       /// </summary>
       /// <value></value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        /// <value></value>
        public int PageSize { get; set; }
    }
}