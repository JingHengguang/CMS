using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Cms.Backend.Api.Model
{  
    
// huawei MatePad 11 夏日胡杨 128G 
//                   海岛蓝
    public class AuditInfo : BaseModel
    {
        
 /// 调用参数
        public string Parameters { get; set; }

        /// 浏览器信息
        public string BrowserInfo { get; set; }

        /// 客户端信息
        public string ClientName { get; set; }

        /// 客户端IP地址
        public string ClientIpAddress { get; set; }

        /// 执行耗时
        public int ExecutionDuration { get; set; }

        /// 执行时间
        public DateTime ExecutionTime { get; set; }

        /// 返回内容
        public string ReturnValue { get; set; }

        /// 异常对象
        public string Exception { get; set; }

        /// 方法名
        public string MethodName { get; set; }

        /// 服务名
        public string ServiceName { get; set; }

        /// 调用者信息
        public string UserInfo { get; set; }

        /// 自定义数据 
        public string CustomData { get; set; }
    }
}