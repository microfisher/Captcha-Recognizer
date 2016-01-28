using System;
using System.Linq;
using FastVerCode;

namespace Wesley.Component.Captcha.Strategies.LianZhong
{
    public class LianZhongStrategy : IStrategy
    {
        public Account Account { private get; set; }

        public LianZhongStrategy(Account account)
        {
            if (account != null)
            {
                this.Account = account;
            }
            else
            {
                this.Account = new Account
                {
                    SoftKey = null,
                    UserName = null,
                    Password = null
                };
            }
        }

        public string Recognize(string filePath)
        {
            if (string.IsNullOrWhiteSpace(this.Account.SoftKey)) throw new Exception("请输入平台软件Key！");
            if (string.IsNullOrWhiteSpace(this.Account.UserName)) throw new Exception("请输入平台普通用户账号！");
            if (string.IsNullOrWhiteSpace(this.Account.Password)) throw new Exception("请输入平台密码！");
            var code = VerCode.RecYZM_A(filePath, Account.UserName, Account.Password, Account.SoftKey).Split('|').ToList();
            var result = code[0];
            return result;
        }
    }
}
