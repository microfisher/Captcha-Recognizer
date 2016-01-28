using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha
{
    public class Account
    {
        public int SoftId { get; set; } // 软件ID

        public int TypeId { get; set; }// 验证码类型

        public string SoftKey { get; set; } //软件Key

        public string UserName { get; set; } //普通用户账号（这里不要输入开发者账号）

        public string Password { get; set; } //用户密码

        public Account()
        {
            
        }

        public Account(int softId, int typeId, string softKey, string userName ,string password)
        {
            this.SoftId = softId;
            this.TypeId = typeId;
            this.SoftKey = softKey;
            this.UserName = userName;
            this.Password = password;
        }
    }
}
