using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Cms.Backend.Api.Params;
using Cms.Backend.Api.Model;
using Cms.Backend.Api.Repository;
using Cms.Backend.Api.Utils;

namespace Cms.Backend.Api.Controllers
{

    [Authorize] //这个标识是 请求要求身份验证。 对于需要登录的网页，服务器返回此响应401，添加到这里表示以下所有的的请求都需要先验证
    [ApiController]
    [Route("[controller]")]

    public class AuditLogController : ControllerBase
    {
        private IConfiguration _configuration;

        //实例化日志实体
        private IRepository<AuditInfo> _auditInfoRepository;

        public AuditLogController(IConfiguration configuration, IRepository<AuditInfo> auditInfoRepository)
        {
            _configuration = configuration;
            _auditInfoRepository = auditInfoRepository;
        }


        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public dynamic GetAllAudit(int currentPage, int pageSize)
        {

            var audits = _auditInfoRepository.Table.ToList();

            var audit = audits.OrderByDescending(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);

            if (audit.Count() <= 0)
            {
                return ResponseStatus.ResponseError("没有找到日志类型");
            }

            dynamic Data = new
            {
                Data = audit,
                Pager = new { currentPage, pageSize, totalSize = audits.Count() }
            };
            return ResponseStatus.ResponseSuccess(Data, "查找所有日志成功");

        }

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Get()
        {
            int currentPage = int.Parse(Request.Query["currentPage"]);
            int pageSize = int.Parse(Request.Query["pageSize"]);

            return GetAllAudit(currentPage, pageSize);
        }
    }
}