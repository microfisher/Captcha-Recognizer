using System;
using System.Text;

namespace Wesley.Component.Captcha.Strategies.DaMaTu
{
    public class DaMaTuStrategy : IStrategy
    {

        public Account Account { private get; set; } = new Account
        {
            TypeId = 0,
            SoftKey = null,
            UserName = null,
            Password = null
        };

        public DaMaTuStrategy()
        {
        }

        public DaMaTuStrategy(Account account)
        {
            if (account != null) this.Account = account;
        }

        public string Recognize(string filePath)
        {
            if (this.Account.TypeId <= 0) throw new Exception("请输入平台验证码类型！");
            if (string.IsNullOrWhiteSpace(this.Account.SoftKey)) throw new Exception("请输入平台软件Key！");
            if (string.IsNullOrWhiteSpace(this.Account.UserName)) throw new Exception("请输入平台普通用户账号！");
            if (string.IsNullOrWhiteSpace(this.Account.Password)) throw new Exception("请输入平台密码！");
            var result = new StringBuilder(30);
            var status = Dama2.D2File(Account.SoftKey, Account.UserName, Account.Password, filePath, 30, (uint)Account.TypeId, result);
            if (status <= 0) throw new Exception("代码（" + status + "）");
            return result.ToString();
        }
    }

}
