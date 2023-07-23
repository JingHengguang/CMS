// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading.Tasks;
// using Cms.Backend.Api.Model;
// using Cms.Backend.Api.Params;
// using Cms.Backend.Api.Repository;
// using Cms.Backend.Api.Utils;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;

// namespace Cms.Backend.Api.Controllers
// {
//     //[Authorize]
//     [ApiController]
//     [Route("[controller]")]
//     public class LikesController : ControllerBase
//     {
//         private IRepository<Likes> _likesRepository;

//         // 实例一个文章的接口
//         private IRepository<Articles> _articleRepository;
//         //实例一个用户接口
//         private IRepository<Users> _usersRepository;

//         public LikesController(IRepository<Likes> likesRepository, IRepository<Articles> articleRepository, IRepository<Users> usersRepository)
//         {
//             _likesRepository = likesRepository;
//             _articleRepository = articleRepository;
//             _usersRepository = usersRepository;
//         }

//         //添加
//         [HttpPost]
//         public dynamic Post(CURLikes newlike)
//         {

//             //判断用户和文章是否存在

//             //通过文章Id获取要修改的文章
//             var article = _articleRepository.GetById(newlike.ArticleId);

//             if (article == null)
//             {
//                 return new
//                 {
//                     Code = 104,
//                     Data = "",
//                     Msg = "你要点赞的文章不存在"
//                 };
//             }
//             //通过用户Id获取当前点赞的用户
//             var user = _usersRepository.GetById(newlike.UserId);

//             if (user == null)
//             {
//                 return new
//                 {
//                     Code = 104,
//                     Data = "",
//                     Msg = "你要点赞的用户不存在"
//                 };
//             }

//             //判断有没有点过赞
//             var likeOK = _likesRepository.Table.Where(x => x.ArticleId == newlike.ArticleId && x.UserId == newlike.UserId).ToList().FirstOrDefault();


//             //有点过赞，修改点赞状态
//             if (likeOK != null)
//             {
//                 //通过id获取喜欢表的内容
//                 // var likes = _likesRepository.GetById(likeOK.Id);

//                 //修改点赞状态
//                 likeOK.applaud = !likeOK.applaud;

//                 _likesRepository.Update(likeOK);


//                 //判断点赞状态是否为true，true+1, falsh-1
//                 if (likeOK.applaud)
//                 {
//                     article.LikesNum += 1;
//                 }
//                 else
//                 {
//                     article.LikesNum -= 1;
//                 }

//                 //将点赞数量更新到文章表
//                 _articleRepository.Update(article);

//                 var updateRes = new
//                 {
//                     Code = 1000,
//                     Data = likeOK,
//                     Msg = "更新数据成功"
//                 };

//                 return JsonHelper.Serialize(updateRes);
//             }

//             //将获取的用户id,文章id添加到likes表
//             var like = new Likes
//             {
//                 UserId = newlike.UserId,
//                 ArticleId = newlike.ArticleId,
//                 applaud = true,
//             };

//             //将用户点赞信息插入likes表
//             _likesRepository.Insert(like);

//             article.LikesNum += 1;

//             //将点赞数量更新到文章表
//             _articleRepository.Update(article);

//             var res = new
//             {
//                 Code = 1000,
//                 Data = like,
//                 Msg = "点赞成功"
//             };

//             //将结果实例化后输出
//             return JsonHelper.Serialize(res);
//         }

       
//     }
// }