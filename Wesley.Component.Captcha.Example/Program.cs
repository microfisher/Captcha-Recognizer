using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Component.Captcha.Strategies.RuoKuai;

namespace Wesley.Component.Captcha.Example
{
    static class Program
    {
        static void Main(string[] args)
        {
            var decoder = new Decoder(Platform.RuoKuai);

            //若此处不设置Account，程序直接读取在策略代码中设置的默认值
            //decoder.Account=new Account
            //{
            //    SoftId = 0, // 软件ID（此ID需要注册开发者账号才可获得）
            //    TypeId = 0, // 验证码类型（四位字符或其他类型的验证码，根据各平台设置不同值）
            //    SoftKey = "", //软件Key （此Key也需要注册开发者账号才可获得）
            //    UserName = "", //账号（此账号为打码平台的普通用户账号，开发者账号不能进行图片识别）
            //    Password = "" //密码
            //};
            decoder.OnStart += (s, e) =>
            {
                Console.WriteLine("验证码（"+e.FilePath+"）识别启动……");
            };
            decoder.OnCompleted += (s, e) =>
            {
                Console.WriteLine("验证码（" + e.FilePath + "）识别完成：" + e.Code + "，耗时：" + (e.Milliseconds/1000) + "秒");
            };
            decoder.OnError += (s, e) =>
            {
                Console.WriteLine("验证码识别出错：" + e.Exception.Message);
            };
            for (var i = 1; i <= 3; i++)
            {
                decoder.Decode("c:\\checkcode"+i+".png");
            }
            Console.ReadKey();
        }
    }
}
