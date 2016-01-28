using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Wesley.Component.Captcha.Strategies.YunDaMa
{
    public class YunDaMaStrategy : IStrategy
    {
        public Account Account { private get; set; }

        public YunDaMaStrategy(Account account)
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
            var result = new StringBuilder(30);
            var status = YunDaMa.YDM_EasyDecodeByPath(Account.UserName, Account.Password, Account.SoftId, Account.SoftKey, filePath, Account.TypeId, 30, result);
            if (status <=0) throw new Exception("代码（" + status + "）");
            return result.ToString();
        }
    }
}
