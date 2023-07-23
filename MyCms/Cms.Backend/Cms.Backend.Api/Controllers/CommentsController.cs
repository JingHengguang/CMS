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
    //  [Authorize] //这个标识是 请求要求身份验证。 对于需要登录的网页，服务器返回此响应401，添加到这里表示以下所有的的请求都需要先验证
    [ApiController]
    [Route("[controller]")]

    public class CommentsController : ControllerBase
    {
        private IConfiguration _configuration;

        //实例化一个评论表的接口
        private IRepository<Comments> _commentRepository;


        private IRepository<Users> _userRepository;



        //构造函数初始化一些值
        public CommentsController(IConfiguration configuration, IRepository<Users> userRepository, IRepository<Comments> commentRepository)
        {
            _configuration = configuration;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }


        //根据文章id获取指定评论
        [HttpGet("{articleId}")]
        public dynamic Get(int articleId)
        {

            //通过id获取指定的评论
            var comment = _commentRepository.Table.Where(x => x.ArticleId == articleId).ToList();

            if (comment.Count == 0)
            {
                return new
                {
                    Code = 104,
                    Data = "",
                    Msg = "当前文章没有评论"
                };
            }

            var user = _userRepository.Table.ToList();

            // var com=comment.Where(x=>x.FromUserId==)

            return new
            {
                Code = 1000,
                Data = new
                {
                    comment,
                    user
                },
                Msg = "获取当前文章的评论成功"
            };
        }


        // 添加评论
        [HttpPost]
        public dynamic Post(CURComment newComment)
        {
            //评论时 用户id、文章id、发布文章用户的id、评论内容不能为空
            var fromUserId = newComment.FromUserId;
            var toUserId = newComment.ToUserId;
            var articleId = newComment.ArticleId;
            var comment = newComment.Comment;

            if (string.IsNullOrEmpty(comment) || fromUserId <= 0 || toUserId <= 0 || articleId <= 0)
            {
                return new
                {
                    Code = 104,
                    Data = "",
                    Msg = "用户id、文章id、发布文章用户id、评论内容不能为空"
                };
            }

            //将评论信息插入到评论表中
            var comments = new Comments
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                ArticleId = articleId,
                Comment = comment
            };

            _commentRepository.Insert(comments);

            var res = new
            {
                Code = 1000,
                Data = comments,
                Msg = "评论成功"
            };

            //将结果实例化后输出
            return JsonHelper.Serialize(res);
            // return res;

        }

    }
}