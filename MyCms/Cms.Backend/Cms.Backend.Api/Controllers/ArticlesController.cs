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

    public class ArticlesController : ControllerBase
    {
        private IConfiguration _configuration;

        //实例化一个文章表的接口
        private IRepository<Articles> _articleRepository;

        //实例化一个类型表的接口
        private IRepository<Types> _typeRepository;

        //实例化一个内容Content表的接口
        private IRepository<Content> _contentRepository;

        //实例化一个Users表的接口
        private IRepository<Users> _usersRepository;

        //实例化一个内容Like表的接口
        private IRepository<Likes> _likesRepository;

        //构造函数初始化一些值
        public ArticlesController(
            IConfiguration configuration,
            IRepository<Articles> articleRepository,
            IRepository<Types> typeRepository,
            IRepository<Content> contentRepository,
            IRepository<Users> usersRepository,
            IRepository<Likes> likesRepository
            )
        {
            _configuration = configuration;
            _articleRepository = articleRepository;
            _typeRepository = typeRepository;
            _contentRepository = contentRepository;
            _usersRepository = usersRepository;
            _likesRepository = likesRepository;
        }

        //查询所有的文章  
        [HttpGet]
        public dynamic Get()
        {
            var currentPage = int.Parse(Request.Query["currentPage"][0]);
            var pageSize = int.Parse(Request.Query["pageSize"][0]);

            // var CmsDb = new CmsBackendDb();
            var articles = _articleRepository.Table.Include(x => x.User).Include(x => x.Content).ToList();

            //   分页倒序查看文章列表
            var article = articles.OrderByDescending(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);


            if (articles.Count == 0)
            {
                return ResponseStatus.ResponseError("没有任何数据");
            }

            dynamic Data = new
            {
                Data = article,
                Pager = new { currentPage, pageSize, totalSize = articles.Count() }
            };
            return ResponseStatus.ResponseSuccess(Data, "获取所有文章成功");
        }


       /// <summary>
       ///  ///根据id获取指定文章
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {

            //通过id获取指定的文章
            var article = _articleRepository.GetById(id);

            if (article == null)
            {

                return ResponseStatus.ResponseError("当前文章不存在");

            }

            //在通过当前文章的contentId获取内容
            var content = _contentRepository.GetById(article.ContentId);

            if (content == null)
            {
                return ResponseStatus.ResponseError("当前文章内容找不到");
            }

            //在通过当前的用户
            var user = _usersRepository.GetById(article.UserId);

            if (user == null)
            {
                return ResponseStatus.ResponseError("找不到当前文章发布的用户");
            }


            var like = _likesRepository.Table.ToList();

            dynamic Data = new
            {
                like,
                article
            };

            return ResponseStatus.ResponseSuccess(Data, "获取当前文章成功");
        }

           
           /// <summary>
           /// 根据用户的id获取对应得文章
           /// </summary>
           /// <param name="userId"></param>
           /// <returns></returns>
           [HttpGet,Route("userId/{userId}")]
          public dynamic getArticlesByUserId(int userId){
                   
            //获取前台传来的参数
            var currentPage=int.Parse(Request.Query["currentPage"][0]);
            var pageSize=int.Parse(Request.Query["pageSize"][0]);


             // 筛选x.UserId==userId  再表连接 
            //   var CmsDb=new CmsBackendDb();
              var articles= _articleRepository.Table.Where(x=>x.UserId==userId).Include(x=>x.User).Include(x=>x.Content);
               
              var articlePaging=articles.OrderByDescending(x=>x.Id).Skip((currentPage-1)*pageSize).Take(pageSize);

             if (articlePaging.Count()<=0)
             {
                  return ResponseStatus.ResponseError("当前用户没有发布过文章");
             }

               dynamic Data=new {
                    Data=articlePaging,
                    Pager = new { currentPage, pageSize, totalSize = articles.Count() }
               };

               return ResponseStatus.ResponseSuccess(Data,"查询成功");
             
          }

        /// <summary>
        /// 获取要查询的文章数据，模糊查询
        /// 
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [HttpPost, Route("queryArticle")]
        public dynamic GetQuery(QueryByArticle queryInfo)
        {
            var querydata = queryInfo.QueryArticleDatas.Trim();

            var CmsDb = new CmsBackendDb();

            var articles = CmsDb.Articles.Include(x => x.User).Include(x => x.Content).ToList().Where(x => x.Title.Contains(querydata));


            if (articles == null)
            {
                return ResponseStatus.ResponseError("没有你要找的数据");

            }
            else
            {
                dynamic Data = new
                {
                    Data = articles
                };
                var res = ResponseStatus.ResponseSuccess(Data, "查询成功");

                //将结果实例化后输出
                return JsonHelper.Serialize(res);

            }



        }

        //添加
        [HttpPost]
        public dynamic Post(CURArticle newArticle)
        {

            // string title = string.IsNullOrWhiteSpace( newArticle.Title.Trim())?;
            // string contentWord = newArticle.Words.Trim();
            // string currentUser = string.IsNullOrEmpty(newArticle.User) ? "admin" : newArticle.User;
            // int currentTypeId = newArticle.TypeId;

            //创建时直接初始化浏览量、点赞、是否推荐,判断是否有传值
            // var readingsNum = newArticle.ReadingsNum > 0 ? newArticle.ReadingsNum : 0;
            // var likesNum = newArticle.LikesNum > 0 ? newArticle.LikesNum : 0;

            //发布文章时 1.标题不能为空 2.内容文字不能为空 3.发布的作者不能为空 4.类型不能为空
            if (string.IsNullOrEmpty(newArticle.Title.Trim()) ||
                 string.IsNullOrEmpty(newArticle.Words.Trim()) ||
                 string.IsNullOrEmpty(newArticle.User.Trim()) ||
                  newArticle.TypeId <= 0)
            {
                return ResponseStatus.ResponseError("文章的标题、内容、发布的用户、类型不能为空");

            }

            string title = newArticle.Title.Trim();
            string contentWord = newArticle.Words.Trim();
            string currentUser = newArticle.User;
            int currentTypeId = newArticle.TypeId;

            //创建时直接初始化浏览量、点赞、是否推荐,判断是否有传值
            var readingsNum = 0;
            var likesNum = 0;

            //判断类型表中是否有当前获取到的类型
            var type = _typeRepository.Table.Where(x => x.Id == currentTypeId);
            //判断用户表中是否有当前获取到的用户
            var user = _usersRepository.Table.Where(x => x.Username == currentUser);


            if (type.ToList().Count == 0 || user.ToList().Count == 0)
            {
                return ResponseStatus.ResponseError("当前类型或用户名无效");
                // new
                // {
                //     Code = 104,
                //     Data = new
                //     {
                //         user = user.ToList(),
                //         type = type.ToList()
                //     },
                //     Msg = "当前类型或用户名无效"
                // };
            }


            //将内容信息插入到内容表中
            var content = new Content
            {
                Words = contentWord,
                Picture = newArticle.Picture,
                Video = newArticle.Video
            };

            //将内容表类容插入到Content表中
            _contentRepository.Insert(content);

            //获取到查询到的用户的ID
            var UserId = user.ToList()[0].Id;

            //获取查询到的内容id(获取最后一条内容的id)
            var ContentId = _contentRepository.Table
            .Where(x => x.Words == contentWord && x.Picture == newArticle.Picture && x.Video == newArticle.Video)
            .ToArray()
            .LastOrDefault().Id;

            var url = FilesController.url;

            //将文章信息插入到文章表中
            var article = new Articles
            {
                Title = title,
                Synopsis = newArticle.Synopsis,
                Remarks = newArticle.Remarks,
                ReadingsNum = readingsNum,
                LikesNum = likesNum,
                IsRecommend = newArticle.IsRecommend,

                //外键id插入
                TypeId = currentTypeId,
                UserId = UserId,
                ContentId = ContentId,
                  
                  //封面图
                CoverPictureUrl = url,
                 

                 //新增审核状态为待审核 
                 IsCheck=0 
                 
                 


            };

            //将文章类容插入到Article表中
            _articleRepository.Insert(article);

            var res = ResponseStatus.ResponseSuccess(article, "添加文章成功");

            //将结果实例化后输出
            return JsonHelper.Serialize(res);

        }

        //查询所有的文章readingsNum倒序  (根据时间倒序)
        [HttpGet(), Route("reading")]
        public dynamic GetReading()
        {
            //当前时间戳
            var currentTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            //48小时前的时间戳
            var TimestampBy48 = currentTimestamp - (24 * 60 * 60 * 1000);

            //获取48小时内的文章并根据文章浏览量倒序排列
            var articles = _articleRepository.Table.ToList()
            .Where(x => new DateTimeOffset(Convert.ToDateTime(x.UpdatedTime))
            .ToUnixTimeMilliseconds() >= TimestampBy48)
            .ToList()
            .OrderByDescending(x => x.ReadingsNum);

            if (articles == null)
            {
                return ResponseStatus.ResponseError("没有任何数据");
            }

            var res = ResponseStatus.ResponseSuccess(articles, "获取48小时内的文章成功");

            return JsonHelper.Serialize(res);
        }



        //查看推荐文章
        [HttpGet(), Route("isRecommend")]
        public dynamic GetisRecommend()
        {
            var articles = _articleRepository.Table.ToList().Where(x => x.IsRecommend == true);

            if (articles == null)
            {
                return ResponseStatus.ResponseError("没有任何数据");

            }


            dynamic Data = new
            {
                Data = articles,
                // Pager = new { currentPage, pageSize, totalSize = articles.Count() }
            };
            var res = ResponseStatus.ResponseSuccess(Data, "获取推荐的文章成功");

            return JsonHelper.Serialize(res);
        }


        //根据id修改文章信息
        [HttpPut("{id}")]
        public dynamic Put(int id, CURArticle UpdateArticle)
        {
            var title = UpdateArticle.Title.Trim();
            var synopsis = UpdateArticle.Synopsis.Trim();


            //修改时浏览量、点赞、是否推荐,判断是否有传值
            var readingsNum = UpdateArticle.ReadingsNum > 0 ? UpdateArticle.ReadingsNum : 0;
            var likesNum = UpdateArticle.LikesNum > 0 ? UpdateArticle.LikesNum : 0;

            //判断是否推荐
            var IsRecommend = UpdateArticle.IsRecommend;

            if (string.IsNullOrEmpty(title))
            {
                return ResponseStatus.ResponseError("文章标题要修改的内容不能为空");             
            }


            //通过Id获取要修改的文章
            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return  ResponseStatus.ResponseError("你要更新的文章不存在");            
            }

            //通过文章的内容id获取内容表的内容
            var content = _contentRepository.GetById(article.ContentId);

            if (content == null)
            {
                return  ResponseStatus.ResponseError("你要更新的内容不存在");          
            }


            content.Picture = UpdateArticle.Picture;
            content.Video = UpdateArticle.Video;

            //将修改的内容更新到内容表
            _contentRepository.Update(content);


            //将要修改的文章内容重新赋给
            article.Title = title;
            article.Synopsis = synopsis;
            article.LikesNum = likesNum;
            article.ReadingsNum = readingsNum;
            article.IsRecommend = IsRecommend;

            var user = _usersRepository.GetById(article.UserId);

            if (user == null)
            {
                return  ResponseStatus.ResponseError("获取不到用户");            
            }

            //更新数据
            _articleRepository.Update(article);

            var res = ResponseStatus.ResponseSuccess(article,"更新数据成功");
           
            return res;
        }


        //根据id删除文章
        [HttpDelete("{id}")]
        public dynamic Dalete(int id)
        {

            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return ResponseStatus.ResponseError(string.Format("无法删除id为{0}的文章", id));
            }

            _articleRepository.Delete(id);

            return  ResponseStatus.ResponseSuccess(article,"删除文章成功");         
        }


        [HttpPut("putRead/{id}")]
        //修改浏览量
        public dynamic PutReadNum(int id)
        {
            //通过Id获取要修改的文章
            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return   ResponseStatus.ResponseError("你要更新的文章不存在");
            }

            article.ReadingsNum += 1;

            _articleRepository.Update(article);

            var res = ResponseStatus.ResponseSuccess(article,"更新浏览量成功");
        
            return res;
        }
    }
}