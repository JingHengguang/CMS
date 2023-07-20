using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Cms.Backend.Api.Params;
using Cms.Backend.Api.Model;
using Cms.Backend.Api.Repository;
using Cms.Backend.Api.Utils;
using Cms.Backend.Db;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cms.Backend.Api.Controllers
{
    [Authorize] //这个标识是 请求要求身份验证。 对于需要登录的网页，服务器返回此响应401，添加到这里表示以下所有的的请求都需要先验证
    [ApiController]
    [Route("[controller]")]

    public class ArticleTypesController : ControllerBase
    {

        private IConfiguration _configuration;

        /// <summary>
        /// 实例化一个类型实体
        /// </summary>       
        private IRepository<Types> _typesRepository;

        /// <summary>
        ///  ///构造函数初始化一些值
        /// </summary>
        /// <param name="configuration"></param>
        public ArticleTypesController(IConfiguration configuration,
        IRepository<Types> typesRepository
        )
        {
            _configuration = configuration;
            _typesRepository = typesRepository;
        }


      /// <summary>
      /// 查找所有类型并分页
      /// </summary>
      /// <param name="currentPage"></param>
      /// <param name="pageSize"></param>
      /// <returns></returns>
      public dynamic TypeData(int currentPage,int pageSize){
            
            var types = _typesRepository.Table.ToList();
            var type = types.OrderByDescending(x=>x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);

            if (type.Count() <= 0)
            {
                return ResponseStatus.ResponseError("没有找到任何类型");
            }

            dynamic Data = new
            {
                Data = type,
                Pager = new { currentPage, pageSize, totalSize = types.Count() }
            };
            return ResponseStatus.ResponseSuccess(Data, "查找所有类型成功");
      }

        //查询所有的类型
        [HttpGet]
        public dynamic Get()
        {

            var currentPage = int.Parse(Request.Query["currentPage"][0]);
            var pageSize = int.Parse(Request.Query["pageSize"][0]);
           
             return TypeData(currentPage,pageSize);
        }

        [HttpPost, Route("GetTypeByTypeName")]
        public dynamic GetTypeByTypeName(CURTypes typeQuery)
        {

            var currentPage = typeQuery.CurrentPage;
            var pageSize = typeQuery.PageSize;

            var typeValue = typeQuery.Type.Trim();

            if (string.IsNullOrWhiteSpace(typeValue))
            {   
               return TypeData(currentPage,pageSize);              
            }

             var types=_typesRepository.Table.Where(x=>x.Type.Contains(typeValue)).ToList();
             var type = types.Skip((currentPage - 1) * pageSize).Take(pageSize);

             dynamic Data = new
            {
                Data = type,
                Pager = new { currentPage, pageSize, totalSize = types.Count() }
            };
            return ResponseStatus.ResponseSuccess(Data, "查找指定的类型成功");
        }


        /// <summary>
        /// 新增类型
        /// </summary>
        /// <param name="addType"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Post(CURTypes addType)
        {

            var currentType = addType.Type.Trim();

            if (string.IsNullOrWhiteSpace(currentType))
            {
                return ResponseStatus.ResponseError("当前类型名称为空值");
            }
            var type = _typesRepository.Table.Where(x => x.Type.Trim() == currentType).ToList();

            if (type.Count() > 0)
            {
                return ResponseStatus.ResponseError("当前类型已存在");
            }


            /// 将新增的类型赋值
            var newType = new Types
            {
                Type = currentType,
                Remarks = addType.Remarks
            };

            //插入到类型表
            _typesRepository.Insert(newType);

            return ResponseStatus.ResponseSuccess(newType, "添加类型成功");

        }


        /// <summary>
        /// 修改类型
        /// </summary>
        /// <param name="updateType"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public dynamic Put(int id, CURTypes updateType)
        {
            var currentType = updateType.Type.Trim();

            if (string.IsNullOrWhiteSpace(currentType))
            {
                return ResponseStatus.ResponseError("当前类型名称为空值");
            }


            var newType = _typesRepository.GetById(id);

            if (newType == null)
            {
                return ResponseStatus.ResponseError("当前类型类型id不存在");
            }

            //如果修改后的类型名称不等于当前id的名称 就说明更改了类型名称
            if (!newType.Type.Trim().Equals(currentType))
            {
                //去类型表里找有没有重复的类型名称
                var type = _typesRepository.Table.Where(x => x.Type.Trim() == currentType).ToList();

                if (type.Count() > 0)
                {
                    return ResponseStatus.ResponseError("当前类型已存在");
                }
            }


            ///将修改的类型赋值
            newType.Type = currentType;
            newType.Remarks = updateType.Remarks;

            _typesRepository.Update(newType);

            return ResponseStatus.ResponseSuccess(newType, "修改类型成功");


        }


        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public dynamic Detale(int id)
        {
            var type = _typesRepository.GetById(id);

            if (type == null)
            {
                return ResponseStatus.ResponseError("当前类型类型id不存在");
            }

            _typesRepository.Delete(id);

            return ResponseStatus.ResponseSuccess(type, "删除类型成功");
        }
    }
}