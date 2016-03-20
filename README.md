# 验证码识别

异步图形验证码识别程序（集成了若快、优优云、打码兔、云打码等人工打码平台）


### 主要特性

- 采用策略设计模式分离各个打码平台；
- 支持异步方式多并发识别；
- 识别完成后自动事件通知；
- 反射方式获取识别策略;
- 人工识别准确率高达99%，平均速度在2-6秒左右，经过我们测试，若快打码识别速度最快；


### 运行截图	

![控制台运行示例](https://github.com/coldicelion/Captcha-Recognizer/raw/master/Wesley.Component.Captcha.Example/Resources/running.jpg?raw=true)


### 当前集成了哪些第三方平台？

- 若快打码 [http://www.ruokuai.com ](http://www.ruokuai.com "若快打码")
- 优优云 [http://www.uuwise.com ](http://www.uuwise.com "优优云")
- 云打码 [http://yundama.com ](http://yundama.com "云打码")
- 打码兔 [http://www.dama2.com ](http://www.dama2.com "打码兔")


### 安装方法

- 首先肯定是要去上面这些平台中的一个注册下账号（开发者账号和普通账号都需要注册）；
- 其次是将开发者账号中的软件ID、软件Key复制出来；
- 然后登陆普通账号充值1~10元；
- 接着将ThirdPartLibrary文件夹下的DLL类库复制到Wesley.Component.Captcha.Example项目下的bin\debug目录下；
- 若该项目下没有bin\debug文件夹，请重新生成解决方案后再复制过去；
- 最后运行Wesley.Component.Captcha.Example示例项目，按注释设置软件ID、软件Key、账号、密码、验证码类型就可以开始识别了；


### 示例代码
	static class Program
    {
        static void Main(string[] args)
        {
            //第一个参数是第三方平台
            //第二个参数是平台账号信息，若此处不设置Account，则需要在策略代码中设置默认值
            var decoder = new Decoder(Platform.RuoKuai, new Account
            {
                SoftId = 0, // 软件ID（此ID需要注册开发者账号才可获得）
                TypeId = 0, // 验证码类型（四位字符或其他类型的验证码，根据各平台设置不同值）
                SoftKey = null, //软件Key （此Key也需要注册开发者账号才可获得）
                UserName = null, //账号（此账号为打码平台的普通用户账号，开发者账号不能进行图片识别）
                Password = null //密码
            });
            decoder.OnStart += (s, e) =>
            {
                Console.WriteLine("验证码（"+e.FilePath+"）识别启动……");
            };
            decoder.OnCompleted += (s, e) =>
            {
                Console.WriteLine("验证码（" + e.FilePath + "）识别完成：" + e.Code + "，耗时：" + (e.Milliseconds/1000) + "秒，线程ID："+e.ThreadId);
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
	


### 如何增加新的验证码平台？

- 在项目里的Strategies文件夹中创建新平台的文件夹，名字随意例如：YouYouYun；
- 在YouYouYun文件夹中创建一个继承至IStrategy接口并且后缀为Strategy的策略类如：YouYouYunStrategy.cs；
- 按照其他文件夹中策略类中的实现方式来实现这个类，如果第三方平台有DLL记得复制到bin\debug目录下去；
- 修改项目中的Platform.cs文件，增加新的枚举类型YouYouYun；
- 程序实例化时将自动查找包含YouYouYun关键字的策略类并实例化，调用其识别方法；


### 技术探讨

QQ号:276679490

微信：coldicelion

