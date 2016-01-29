using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha.Strategies.YouYouYun
{
    public class YouYouYunStrategy : IStrategy
    {
        public Account Account { private get; set; }

        public YouYouYunStrategy(Account account)
        {
            if (account != null)
            {
                this.Account = account;
            }
            else
            {
                this.Account = new Account
                {
                    SoftId = 0,
                    TypeId = 0,
                    SoftKey = null,
                    UserName = null,
                    Password = null
                };
            }
        }

        public string Recognize(string filePath)
        {
            if (this.Account.SoftId <= 0) throw new Exception("请输入平台软件ID！");
            if (this.Account.TypeId <= 0) throw new Exception("请输入平台验证码类型！");
            if (string.IsNullOrWhiteSpace(this.Account.SoftKey)) throw new Exception("请输入平台软件Key！");
            if (string.IsNullOrWhiteSpace(this.Account.UserName)) throw new Exception("请输入平台普通用户账号！");
            if (string.IsNullOrWhiteSpace(this.Account.Password)) throw new Exception("请输入平台密码！");

            var sb = new StringBuilder(50);
            YouYouYun.uu_easyRecognizeFile(this.Account.SoftId, this.Account.SoftKey, this.Account.UserName, this.Account.Password, filePath, this.Account.TypeId, sb);
            var code = sb.ToString().Split('_');
            var result = code[1];
            return result;
        }

    }
}
