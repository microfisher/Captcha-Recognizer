using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using Wesley.Component.Captcha.Events;
using Wesley.Component.Captcha.Strategies.RuoKuai;
using System.IO;

namespace Wesley.Component.Captcha
{
    public class Decoder : IDecoder
    {

        public event EventHandler<OnErrorEventArgs> OnError;
        public event EventHandler<OnStartedEventArgs> OnStart;
        public event EventHandler<OnCompletedEventArgs> OnCompleted;

        private readonly Account _account;
        private readonly Platform _platform;
        private readonly IStrategy _strategy;

        public Decoder(Platform platform)
        {
            this._platform = platform;
            this._strategy = GetStrategy();
        }

        public Decoder(Platform platform,Account account)
        {
            this._account = account;
            this._platform = platform;
            this._strategy = GetStrategy();
        }

        private IStrategy GetStrategy()
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(type => typeof(IStrategy).IsAssignableFrom(type) && !type.IsAbstract && type.FullName.ToLower().Contains(this._platform.ToString().ToLower()));
            return Activator.CreateInstance(assembly, this._account) as IStrategy;
        }

        public async Task Decode(string filePath)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(filePath)) throw new Exception("图片文件路径不能为空！");
                    if (!File.Exists(filePath)) throw new Exception("验证码图片文件（" + filePath + "）不存在！");
                    if (this._strategy == null) throw new Exception("不能实例化（" + this._platform + "）平台的识别策略！");
                    if (OnStart != null) this.OnStart(this, new OnStartedEventArgs(filePath));
                    var startTime = DateTime.Now;
                    var code = this._strategy.Recognize(filePath);
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    var millisecond = DateTime.Now.Subtract(startTime).TotalMilliseconds;
                    if (OnCompleted != null) this.OnCompleted(this, new OnCompletedEventArgs(code, millisecond, filePath, threadId));
                }
                catch (Exception ex)
                {
                    if (OnError != null) this.OnError(this, new OnErrorEventArgs(filePath, ex));
                }
            });
        }
    }
}
