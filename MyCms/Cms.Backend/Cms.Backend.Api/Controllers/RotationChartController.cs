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
   // [Authorize] //这个标识是 请求要求身份验证。 对于需要登录的网页，服务器返回此响应401，添加到这里表示以下所有的的请求都需要先验证
    [ApiController]
    [Route("[controller]")]

    public class RotationChartController : ControllerBase
    {

        private IConfiguration _configuration;

        /// <summary>
        /// 实例化一个轮播图实体
        /// </summary>       
        private IRepository<Broadcast_map> _broadcast_mapRepository;

        /// <summary>
        ///  ///构造函数初始化一些值
        /// </summary>
        /// <param name="configuration"></param>
        public RotationChartController(IConfiguration configuration, IRepository<Broadcast_map> broadcast_mapRepository)
        {
            _configuration = configuration;
            _broadcast_mapRepository = broadcast_mapRepository;
        }


        /// <summary>
        /// 查找所有轮播图并分页
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public dynamic BroadcastPicData(int currentPage, int pageSize)
        {

            var broadcastPics = _broadcast_mapRepository.Table.ToList();
            var broadcastPic = broadcastPics.OrderByDescending(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);

            if (broadcastPic.Count() <= 0)
            {
                return ResponseStatus.ResponseError("没有找到任何轮播图");
            }

            dynamic Data = new
            {
                Data = broadcastPic,
                Pager = new { currentPage, pageSize, totalSize = broadcastPics.Count() }
            };
            return ResponseStatus.ResponseSuccess(Data, "查找所有轮播图成功");
        }

        //查询所有的轮播图
        [HttpGet]
        public dynamic Get()
        {

            var currentPage = int.Parse(Request.Query["currentPage"][0]);
            var pageSize = int.Parse(Request.Query["pageSize"][0]);

            return BroadcastPicData(currentPage, pageSize);
        }

        [HttpPost("GetRotationChartsByremark")]
        public dynamic GetRotationChartsByremark(CURRotationCharts queryRemark)
        {

            var currentPage = queryRemark.CurrentPage;
            var pageSize = queryRemark.PageSize;

            var Remarks = queryRemark.Remarks.Trim();

            if (pageSize <= 0)
            {
                return ResponseStatus.ResponseError("当前页的显示数量为" + pageSize + "你在开玩笑?");
            }

            if (string.IsNullOrWhiteSpace(Remarks))
            {
              //将当前页 和页面数量传入 显示所有数据
                 return BroadcastPicData(currentPage, pageSize);
            }


            var broadcastPic = _broadcast_mapRepository.Table.Where(x => x.Remarks.Contains(Remarks));
            
  
           dynamic Data = new
            {
                Data = broadcastPic,
                Pager = new { currentPage, pageSize, totalSize = broadcastPic.Count() }
            };

            return ResponseStatus.ResponseSuccess(Data,"查询轮播图成功");

        }

        /// <summary>
        /// 新增类型
        /// </summary>
        /// <param name="addRotationCharts"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Post(CURRotationCharts addRotationCharts)
        {

            var currentPicture = addRotationCharts.Picture.Trim();

            if (string.IsNullOrWhiteSpace(currentPicture))
            {
                return ResponseStatus.ResponseError("当前轮播图路径为空值");
            }

            // var bm = _broadcast_mapRepository.Table.Where(x => x.Picture.Trim() == currentRotationChars).ToList();
            // if (bm.Count() > 0)
            // {
            //     return ResponseStatus.ResponseError("当前类型已存在");
            // }


            /// 将新增的类型赋值
            var newRotationChart = new Broadcast_map
            {
                Picture = currentPicture,
                Remarks = addRotationCharts.Remarks
            };

            //插入到类型表
            _broadcast_mapRepository.Insert(newRotationChart);

            return ResponseStatus.ResponseSuccess(newRotationChart, "添加类型成功");

        }


        /// <summary>
        /// 修改类型
        /// </summary>
        /// <param name="updateRotationCharts"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public dynamic Put(int id, CURRotationCharts updateRotationCharts)
        {
            var currentPicture = updateRotationCharts.Picture.Trim();

            if (string.IsNullOrWhiteSpace(currentPicture))
            {
                return ResponseStatus.ResponseError("当前轮播图路径为空值");
            }


            var newRotationChart = _broadcast_mapRepository.GetById(id);

            if (newRotationChart == null)
            {
                return ResponseStatus.ResponseError("当前轮播图id不存在");
            }

            ///将修改的轮播图赋值
            newRotationChart.Picture = currentPicture;
            newRotationChart.Remarks = updateRotationCharts.Remarks;


            _broadcast_mapRepository.Update(newRotationChart);

            return ResponseStatus.ResponseSuccess(newRotationChart, "修改轮播图成功");


        }


        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public dynamic Detale(int id)
        {
            var rotationChart = _broadcast_mapRepository.GetById(id);

            if (rotationChart == null)
            {
                return ResponseStatus.ResponseError("当前轮播图的id不存在");
            }

            _broadcast_mapRepository.Delete(id);

            return ResponseStatus.ResponseSuccess(rotationChart, "删除类型成功");
        }

    }
}