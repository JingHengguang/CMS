// using System.Linq;
// using Cms.Backend.Api.Model;
// using Cms.Backend.Api.Params;
// using Cms.Backend.Api.Repository;
// using Microsoft.AspNetCore.Mvc;
// using Cms.Backend.Api.Utils;

// namespace Cms.Backend.Api.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class RegisterController : ControllerBase
//     {
//         private IRepository<Users> _usersRepository;

//         public RegisterController(IRepository<Users> usersRepository)
//         {
//             _usersRepository = usersRepository;
//         }

//          [HttpPost]
//         public dynamic Post(CURUser querydata)
//         {
//               if (string.IsNullOrWhiteSpace(querydata.Username.Trim()) ||
//                   string.IsNullOrWhiteSpace(querydata.Password.Trim()) ||
//                   string.IsNullOrWhiteSpace(querydata.checkPass.Trim())||
//                   querydata.UserRoleId<=0)
//               {  

//                  return   ResponseStatus.ResponseError("请检查账号密码是否填写错误");           
//               }

//             var users = _usersRepository.Table.Where(x => x.Username == querydata.Username);

//             if (users.ToList().Count > 0)
//             {  
//               return   ResponseStatus.ResponseError("当前用户已存在");
//             }

//             if (querydata.Password != querydata.checkPass)
//             {

//             return   ResponseStatus.ResponseError("两次输入的密码不一致，请注意是否误输入了空格");

//             }
            
//             /// <summary>
//             /// 获取传入的头像地址
//             /// </summary>
//             /// <value></value>
//             // var avatarUrl= FilesController.avatarUrl;

//             var userAvatarUrl= string.IsNullOrWhiteSpace(querydata.AvatarUrl)?"UploadFiles\\Images\\default.jpg":querydata.AvatarUrl;

//             var user = new Users
//             {
//                 Username = querydata.Username,
//                 Password = querydata.Password,
//                 UserRoleId = querydata.UserRoleId,
//                 Remarks=querydata.Remarks,
//                 AvatarUrl=userAvatarUrl,
//             };

//             _usersRepository.Insert(user);

//           return   ResponseStatus.ResponseSuccess(user,"注册用户成功");


//             // return JsonHelper.Serialize(res);

//         }
//     }
// }