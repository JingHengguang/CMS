using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
 
namespace Cms.Backend.Filters
{
    public class CmsLogs 
    {
        private readonly ILogger<CmsLogs> _logger;
 
        //构造函数声明注入
        public CmsLogs(ILogger<CmsLogs> logger)
        {
            _logger = logger;
        }
            
        //使用
        public void TestLog()
        {
            try
            {
                _logger.LogInformation("TestLog",null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), "");
            }
        }
    }
}