namespace Cms.Backend.Api.Params
{
    public class CURTypes
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        /// <value></value>
        public string Type { get; set; }

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