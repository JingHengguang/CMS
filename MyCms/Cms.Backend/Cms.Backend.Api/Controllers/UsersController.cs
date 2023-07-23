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

namespace Cms.Backend.Api.Controllers
{
    //[Authorize] //这个标识是 请求要求身份验证。 对于需要登录的网页，服务器返回此响应401，添加到这里表示以下所有的的请求都需要先验证
    [ApiController]
    [Route("[controller]")]


    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepository<Users> _usersRepository;
        private IRepository<UserRoles> _userRolesRepository;

        //初始化token
        private TokenParameter _toKenParameter;

        public UsersController(IConfiguration configuration, IRepository<Users> usersRepository, IRepository<UserRoles> userRolesRepository)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
            _userRolesRepository = userRolesRepository;
            _toKenParameter =
                  _configuration
                      .GetSection("TokenParameter")
                      .Get<TokenParameter>();
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Get()
        {
            var currentPage = int.Parse(Request.Query["currentPage"][0]);  //  1,
            var pageSize = int.Parse(Request.Query["pageSize"][0]);  //   10,

            if (currentPage <= 0 || pageSize <= 0)
            {
                return ResponseStatus.ResponseError("当前页或当前页的显示数量必须大于0");
            }
            var users = _usersRepository.Table.Include(x => x.UserRole).ToList();

            //   分页
            var user = users.Skip((currentPage - 1) * pageSize).Take(pageSize);

            dynamic Data = new
            {
                Data = user,
                Pager = new { currentPage, pageSize, totalSize = users.Count() }
            };

            return ResponseStatus.ResponseSuccess(Data, "获取所有用户成功");


        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public dynamic GetUserById(int id)
        {
            var user = _usersRepository.GetById(id);

            if (user == null)
            {
                return ResponseStatus.ResponseError("当前用户不存在");
            }

            return ResponseStatus.ResponseSuccess(user, "获取当前用户成功");
        }


        ///模糊查询用户
        [HttpPost, Route("queryUser")]  //获取查询的数据。
        public dynamic GetQuery(QuerysByUser queryInfo)
        {

            var username = queryInfo.Username.Trim();
            var currentPage = queryInfo.CurrentPage <= 0 ? 1 : queryInfo.CurrentPage;
            var pageSize = queryInfo.PageSize <= 0 ? 5 : queryInfo.PageSize;

            var users = _usersRepository.Table.Where(x => x.Username.Contains(username));
            var user = users.Include(x => x.UserRole).Skip((currentPage - 1) * pageSize).Take(pageSize);

            if (user != null)
            {
                dynamic Data = new
                {
                    Data = user,
                    Pager = new { currentPage, pageSize, totalSize = users.Count() }
                };

                return ResponseStatus.ResponseSuccess(Data, "获取指定用户成功");
            }

            return ResponseStatus.ResponseError("没有你要找的数据");

        }

         /// <summary>
         ///  添加用户
         /// </summary>
         /// <param name="newUser"></param>
         /// <returns></returns>
        [HttpPost]
        public dynamic Post(CURUser newUser)
        {
            var username = newUser.Username.Trim();
            var password = newUser.Password.Trim();

            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return ResponseStatus.ResponseError("账号或密码不能为空");
            }
            
            //判断当前用户是否存在
            var currentUser=_usersRepository.Table.Where(x=>x.Username==username).ToList();
             
             if (currentUser.Count()>0)
             {
                  return ResponseStatus.ResponseError("当前用户已存在");
             }



            var user = new Users
            {
                Username = username,
                Password = password,
                Remarks = newUser.Remarks,
                UserRoleId = newUser.UserRoleId,
            };

            //判断是否有上传头像
            if (!string.IsNullOrWhiteSpace(newUser.AvatarUrl.Trim()))
            {
                user.AvatarUrl = newUser.AvatarUrl;
            }
            else
            {
                user.AvatarUrl = "UploadFiles\\DefaultImage\\default.jpg";
            }

            //将账号密码插入到Users表中
            _usersRepository.Insert(user);
            // var context = new CmsBackendDb();
            var us = _usersRepository.Table.Include(x => x.UserRole).OrderBy(x => x.Id).LastOrDefault();

            return ResponseStatus.ResponseSuccess(us, "创建用户成功");


        }

        //根据id修改用户信息
        [HttpPut("{id}")]
        public dynamic Put(int id, CURUser updateUser)
        {
            var username = updateUser.Username.Trim();

            //判断密码是否为空，若为空则设置默认值等于113
            // var password = string.IsNullOrEmpty(updateUser.Password) ? "113" : updateUser.Password.Trim();
            var password = updateUser.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return ResponseStatus.ResponseError("账号或密码不能为空");
            }


            //通过Id获取要修改的用户
            var user = _usersRepository.GetById(id);

            if (user == null)
            {
                return ResponseStatus.ResponseError("你要更新的用户不存在");
            }

            var userRole = _userRolesRepository.GetById(updateUser.UserRoleId);

            if (userRole == null)
            {
                return ResponseStatus.ResponseError("没有获取到用户角色");
            }



            //将要修改的账号密码重新赋给user
            user.Username = username;
            user.Password = password;
            user.Remarks = updateUser.Remarks;
            user.UserRoleId = updateUser.UserRoleId;

            if (!string.IsNullOrWhiteSpace(updateUser.AvatarUrl.Trim()))
            {
                user.AvatarUrl = updateUser.AvatarUrl;
            }


            //更新数据
            _usersRepository.Update(user);


            dynamic Data = new
            {
                userRole,
                user,
            };

            var res = ResponseStatus.ResponseSuccess(Data, "更新数据成功");

            return JsonHelper.Serialize(res);

        }

        //根据id删除用户
        [HttpDelete("{id}")]
        public dynamic Dalete(int id)
        {

            var user = _usersRepository.GetById(id);

            if (user == null)
            {
                return ResponseStatus.ResponseError(string.Format("无法删除id为{0}的用户", id));
            }

            _usersRepository.Delete(id);

            return ResponseStatus.ResponseSuccess("", "删除用户成功");
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("token")]
        public dynamic GetToken(CURUser newUser)
        {
            var username = newUser.Username.Trim();
            var password = newUser.Password.Trim();

            var user =
                _usersRepository
                    .Table
                    .Where(x =>
                        x.Username == username && x.Password == password)
                    .FirstOrDefault();


            if (user == null)
            {
                return ResponseStatus.ResponseError("用户名或密码不正确，请确认后重试");
            }

            var token =
                ToKenHelper.GenerateToekn(_toKenParameter, user.Username);
            var refreshToken = "112358";

            dynamic Data = new { Token = token, refreshToken = refreshToken, user };
            return ResponseStatus.ResponseSuccess(Data, "用户登录成功^_^");
        }


        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("refreshtoken")]
        public dynamic RefreshToken(RefreshTokenDTO refresh)
        {
            var username = ToKenHelper.ValidateToken(_toKenParameter, refresh);

            if (string.IsNullOrEmpty(username))
            {
                return ResponseStatus.ResponseError("token验证失败");
            }

            var token = ToKenHelper.GenerateToekn(_toKenParameter, username);
            var refreshToken = "121381";


            dynamic Data = new { Token = token, refreshToken = refreshToken };
            return ResponseStatus.ResponseSuccess(Data, "刷新token成功^_^");
        }

    }

}