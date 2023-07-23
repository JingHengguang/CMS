using System;
using System.Collections.Generic;
using System.IO;
using Cms.Backend.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Cms.Backend.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {   
        /// <summary>
        /// 文章封面图片路径
        /// </summary>
        /// <value></value>
        public static string url { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        /// <value></value>
        public static string avatarUrl { get; set; }


        /// <summary>
        /// 轮播图路径
        /// </summary>
        /// <value></value>
        public static string rotationChartUrl { get; set; }
        private readonly IConfiguration _configuration;

        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// 获取文章封面图片
        /// </summary>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        [HttpGet("picPath")]
        public dynamic Get(string picUrl)
        {
            if (picUrl=="null")
            {
                return "";
            }
            else
            {
                var image = System.IO.File.OpenRead(picUrl);
                return File(image, "image/jpeg");
            }
        }
           
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="avatarUrl"></param>
        /// <returns></returns>
        [HttpGet("avatarPath")]
        public dynamic GetAvatar(string avatarUrl)
        {
            if (avatarUrl=="null")
            {
                return "";
            }
            else
            {
                var image = System.IO.File.OpenRead(avatarUrl);
                return File(image, "image/jpeg");
            }
        }

       /// <summary>
        /// 获取轮播图
        /// </summary>
        /// <param name="RotationChartUrl"></param>
        /// <returns></returns>
        [HttpGet("RotationChart")]
        public dynamic GetRotationChart(string RotationChartUrl)
        {
            if (RotationChartUrl=="null")
            {
                return "";
            }
            else
            {
                var image = System.IO.File.OpenRead(RotationChartUrl);
                return File(image, "image/jpeg");
            }
        }

        /// <summary>
        /// 文章封面上传接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("uploadfiles")]
        public string UploadFiles(IFormCollection model)
        {  
            // 获得当前应用所在的完整路径（绝对地址）
            var filePath = Directory.GetCurrentDirectory();

            // 通过配置文件获得存放文件的相对路径
            string path = _configuration["UploadFilesPath"];

            // 最终存放文件的完整路径
            var preFullPath = Path.Combine(filePath, path);
            // 如果路径不存在，则创建
            if (!Directory.Exists(preFullPath))
            {
                Directory.CreateDirectory(preFullPath);
            }

            var resultPath = new List<string>();
            foreach (IFormFile file in model.Files)
            {
                if (file.Length > 0)
                {
                    var fileName = file.FileName;
                    var extName = fileName.Substring(fileName.LastIndexOf("."));//extName包含了“.”
                    var tempPath = Path.Combine(path, Guid.NewGuid().ToString("N") + extName);
                    using (var stream = new FileStream(Path.Combine(filePath, tempPath), FileMode.CreateNew))//Path.Combine(_env.WebRootPath, fileName)
                    {
                        file.CopyTo(stream);

                    }

                    // 此处地址可能带有两个反斜杠，虽然也能用，比较奇怪，统一转换成斜杠，这样在任何平台都有一样的表现
                    resultPath.Add(tempPath.Replace("\\", "/"));
                    url = resultPath[0];
                }
            }

           
            var  res=  ResponseStatus.ResponseSuccess(url,"上传成功");

            return JsonHelper.Serialize(res);
        }

     
        /// <summary>
        /// 上传头像接口
        /// </summary>
        /// <param name="AvatarModel"></param>
        /// <returns></returns>
        [HttpPost, Route("uploadAvatar")]
       public string AvatarUploadFiles(IFormCollection AvatarModel){

              dynamic res;
             
            //获取当前项目所在的绝对路径
           string filePath=Directory.GetCurrentDirectory();
           
           //获取配置文件下存放头像的相对路径
            string path=_configuration["UploadFilesPath"];

            //获取最终头像要放入那个文件夹的   完整路径
            string fullPath=Path.Combine(filePath,path);
            
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                    res = new
                        {
                            Code = 104,
                            Data = new {fullPath},
                            Msg = "保存头像的完整路径获取失败"
                        };

                    return JsonHelper.Serialize(res);
            }
            
             // 如果路径不存在，则创建
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            var resultPath = new List<string>();

            foreach (IFormFile file in AvatarModel.Files){
                if (file.Length > 0)
                {
                    var fileName = file.FileName;
                    var extName = fileName.Substring(fileName.LastIndexOf("."));//extName包含了“.”
                    var tempPath = Path.Combine(path, Guid.NewGuid().ToString("N") + extName);
                    using (var stream = new FileStream(Path.Combine(filePath, tempPath), FileMode.CreateNew))
                    {
                        file.CopyTo(stream);

                    }

                    // 此处地址可能带有两个反斜杠，虽然也能用，比较奇怪，统一转换成斜杠，这样在任何平台都有一样的表现
                    resultPath.Add(tempPath.Replace("\\", "/"));
                    avatarUrl = resultPath[0];
                }
            }
            

            res =  ResponseStatus.ResponseSuccess(avatarUrl,"头像上传成功");
      
            return JsonHelper.Serialize(res);
       }
    
             /// <summary>
        /// 轮播图上传接口
        /// </summary>
        /// <param name="RotationChartModel"></param>
        /// <returns></returns>
        [HttpPost, Route("uploadRotationChart")]
        public string UploadRotationChart(IFormCollection RotationChartModel)
        {  
            // 获得当前应用所在的完整路径（绝对地址）
            var filePath = Directory.GetCurrentDirectory();

            // 通过配置文件获得存放文件的相对路径
            string path = _configuration["UploadFilesPath"];

            // 最终存放文件的完整路径
            var preFullPath = Path.Combine(filePath, path);
            // 如果路径不存在，则创建
            if (!Directory.Exists(preFullPath))
            {
                Directory.CreateDirectory(preFullPath);
            }

            var resultPath = new List<string>();
            foreach (IFormFile file in RotationChartModel.Files)
            {
                if (file.Length > 0)
                {
                    var fileName = file.FileName;
                    var extName = fileName.Substring(fileName.LastIndexOf("."));//extName包含了“.”
                    var tempPath = Path.Combine(path, Guid.NewGuid().ToString("N") + extName);
                    using (var stream = new FileStream(Path.Combine(filePath, tempPath), FileMode.CreateNew))//Path.Combine(_env.WebRootPath, fileName)
                    {
                        file.CopyTo(stream);

                    }

                    // 此处地址可能带有两个反斜杠，虽然也能用，比较奇怪，统一转换成斜杠，这样在任何平台都有一样的表现
                    resultPath.Add(tempPath.Replace("\\", "/"));
                    rotationChartUrl = resultPath[0];
                }
            }
           
            var  res=  ResponseStatus.ResponseSuccess(rotationChartUrl,"上传成功");

            return JsonHelper.Serialize(res);
        }

       }
}
