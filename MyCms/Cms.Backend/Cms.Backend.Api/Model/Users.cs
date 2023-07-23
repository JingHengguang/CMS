using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cms.Backend.Api.Model
{
    public class Users : BaseModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        // 外连接UserRole表   以UserRoleIdId作为外键
        public virtual UserRoles UserRole { get; set; }
        public int UserRoleId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <value></value>
        [Required]
        public string Avatar { get; set; }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <value></value>
        [MaxLength(11)]
        [Required]
        public string UserPhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        /// <value></value>
        public string eMail { get; set; }

        /// <summary>   
        /// 年龄
        /// </summary>
        /// <value></value>
        public int Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// <value></value>
        public string Gender { get; set; }

        /// 绑定的QQ
        /// </summary>
        /// <value></value>
        public string QQNumber { get; set; }

        /// <summary>
        ///微信
        /// </summary>
        /// <value></value>
        public string WeChat { get; set; }


        /// <summary>
        /// 评论表外键
        /// </summary>
        /// <value></value>
        // public IEnumerable<Comments> Comments { get; set; }

        /// <summary>
        /// 点赞表外键
        /// </summary>
        /// <value></value>
        public IEnumerable<Likes> Likes { get; set; }

        //文章表外键
        public IEnumerable<Articles> Article { get; set; }
    }
}