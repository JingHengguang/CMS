namespace Cms.Backend.Api.Params
{
    public class CURUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string checkPass { get; set; }
        public string Remarks { get; set; }
        public int UserRoleId { get; set; }

        /// <summary>
        /// 头像路由
        /// </summary>
        /// <value></value>
        public string AvatarUrl{ get; set; }

    }
}