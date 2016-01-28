# 验证码识别

异步图形验证码识别程序（封装集成了若快、打码兔、联众、云打码等人工打码平台）


### 主要特性

- 采用策略设计模式分离各个打码平台；
- 支持异步方式多并发识别；
- 识别完成后自动事件通知；
- 反射方式获取识别策略;
- 人工识别准确率高达99%，平均速度在2-6秒左右，经测试若快打码速度最快；


### 程序运行截图	

![控制台运行示例](https://github.com/coldicelion/Captcha-Recognizer/raw/master/Wesley.Component.Captcha.Example/Resources/running.jpg?raw=true)


### 第三方平台
- 若快打码 [http://www.ruokuai.com ](http://www.ruokuai.com "若快打码")
- 云打码 [http://yundama.com ](http://yundama.com "云打码")
- 打码兔 [http://www.dama2.com ](http://www.dama2.com "打码兔")
- 联众打码 [http://www.jsdati.com ](http://www.jsdati.com "联众打码")


### 控制台示例代码
	static class Program
    {
        static void Main(string[] args)
        {
            //若此处不设置Account，程序直接读取在策略代码中设置的默认值
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
1.在Wesley.Component.Captcha项目里的Strategies文件夹中创建新平台的文件夹，名字随意，例如：YouYouYun；

2.在YouYouYun文件夹中创建一个继承至IStrategy接口并且后缀为Strategy的策略类如：YouYouYunStrategy.cs；

3.按照其他文件夹中策略类中的方式实现这个类；

4.修改项目中的Platform.cs文件，增加新的枚举类型YouYouYun；

5.程序自动实例化时将自动查找包含YouYouYun关键字的策略类并实例化，调用其验证码的识别方法，至此新的平台就添加成功了；

 

### 注意事项

1.文件夹ThirdPartLibrary下的DLL文件是验证码识别平台的接口类库，需要放置在Wesley.Component.Captcha的bin\debug目录下方可执行识别。其中的FastVerCode.dll是联众打码的接口类库，需要引用至项目中；其他DLL均不需要引用，直接放置在bin\debug即可。

