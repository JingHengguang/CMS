namespace Cms.Backend.Api.Params
{
    public class CURRotationCharts
    {
        /// <summary>
        /// 轮播图路径
        /// </summary>
        /// <value></value>
        public string Picture { get; set; }

        /// <summary>
        ///  //备注
        /// </summary>
        /// <value></value>
        public string Remarks { get; set; }

       
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