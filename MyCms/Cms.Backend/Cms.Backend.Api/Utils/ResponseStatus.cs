


namespace Cms.Backend.Api.Utils{
      

    static public class ResponseStatus
      {
             /// 200- 请求成功
             /// 104- 请求失败
             /// 301- 资源或网页被转移到其他URL
             /// 404- 请求资源网页不存在
             /// 500- 内部服务器错误

           /// <summary>
           ///  请求失败
           /// </summary>
           /// <param name="code"></param>
           /// <param name="data"></param>
           /// <param name="msg"></param>
           /// <returns></returns>
          static public dynamic ResponseError(string msg)
          {
            return new
            {
                Code = 104,
                Data ="",
                Msg = msg
            };
        }

           /// <summary>
           ///   请求成功
           /// </summary>
           /// <param name="code"></param>
           /// <param name="data"></param>
           /// <param name="msg"></param>
           /// <returns></returns>
            static public dynamic ResponseSuccess(dynamic data,string msg)
            {
                    return new
                    {
                        Code = 200,
                        Data =data,
                        Msg = msg
                    };
           }
          
      } 

}