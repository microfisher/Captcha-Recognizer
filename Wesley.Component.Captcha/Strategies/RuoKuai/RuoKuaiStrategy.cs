using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace Wesley.Component.Captcha.Strategies.RuoKuai
{
    public class RuoKuaiStrategy : IStrategy
    {
        public Account Account { private get; set; } = new Account
        {
            SoftId = 0,
            TypeId = 0,
            SoftKey = null,
            UserName = null,
            Password = null
        };

        public RuoKuaiStrategy()
        {
        }

        public RuoKuaiStrategy(Account account)
        {
            if(account!=null)this.Account = account;
        }

        public string Recognize(string filePath)
        {
            if (this.Account.SoftId <= 0) throw new Exception("请输入平台软件ID！");
            if (this.Account.TypeId <= 0) throw new Exception("请输入平台验证码类型！");
            if (string.IsNullOrWhiteSpace(this.Account.SoftKey)) throw new Exception("请输入平台软件Key！");
            if (string.IsNullOrWhiteSpace(this.Account.UserName)) throw new Exception("请输入平台普通用户账号！");
            if (string.IsNullOrWhiteSpace(this.Account.Password)) throw new Exception("请输入平台密码！");
            var param = new Dictionary<object, object>
            {
                {"username",Account.UserName},
                {"password",Account.Password},
                {"typeid",Account.TypeId},
                {"timeout","90"},
                {"softid",Account.SoftId},
                {"softkey",Account.SoftKey}
            };
            var data = File.ReadAllBytes(filePath);
            var response = RuoKuaiHttp.Post("http://api.ruokuai.com/create.json", param, data);
            dynamic json = JsonConvert.DeserializeObject(response);
            return json.Result;
        }
    }
}
