namespace Cms.Backend.Api.Params
{
    public class CURUser
    {
        public string Username { get; set; }

        /// <summary>
        /// 原密码
        /// </summary>
        /// <value></value>
        public string oldPassword { get; set; }
        public string Password { get; set; }

        public string Remarks { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        /// <value></value>
        public int UserRoleId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <value></value>
        public string Avatar { get; set; }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <value></value>
        public string UserPhoneNumber { get; set; }

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
        /// 邮箱
        /// </summary>
        /// <value></value>
        public string eMail { get; set; }
    }
}