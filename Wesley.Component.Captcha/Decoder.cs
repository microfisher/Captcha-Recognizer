using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using Wesley.Component.Captcha.Events;
using Wesley.Component.Captcha.Strategies.RuoKuai;

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
            this._strategy = InitializeStrategy();
        }

        public Decoder(Platform platform,Account account)
        {
            this._account = account;
            this._platform = platform;
            this._strategy = InitializeStrategy();
        }

        private IStrategy InitializeStrategy()
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(type => typeof(IStrategy).IsAssignableFrom(type) && !type.IsAbstract && type.FullName.ToLower().Contains(this._platform.ToString().ToLower()));
            return Activator.CreateInstance(assembly, this._account) as IStrategy;
        }

        public async Task Decode(string filePath)
        {
            if (OnStart != null) this.OnStart(this, new OnStartedEventArgs(filePath));
            await Task.Run(() =>
            {
                try
                {
                    var startTime = DateTime.Now;
                    if (this._strategy == null) throw new Exception("不能实例化此平台（" + this._platform + "）的识别策略！");
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
