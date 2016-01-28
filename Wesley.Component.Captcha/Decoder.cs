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
        public Account Account { get; set; }

        public event EventHandler<OnErrorEventArgs> OnError;
        public event EventHandler<OnStartedEventArgs> OnStart;
        public event EventHandler<OnCompletedEventArgs> OnCompleted;

        private readonly Platform _platform;
        private readonly IStrategy _strategy;

        public Decoder(Platform platform)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type => typeof(IStrategy).IsAssignableFrom(type) && !type.IsAbstract && type.FullName.ToLower().Contains(this._platform.ToString().ToLower()));
                if (assembly == null) throw new Exception("获取此平台（" + this._platform + "）的策略失败！");
                this._strategy = Activator.CreateInstance(assembly, this.Account) as IStrategy;
                if (this._strategy == null) throw new Exception("实例化此平台（" + this._platform + "）的识别策略失败！");
                this._platform = platform;
            }
            catch (Exception ex)
            {
                if (OnError != null) this.OnError(this, new OnErrorEventArgs(null, ex));
            }

        }

        public async Task Decode(string filePath)
        {
            if (OnStart != null) this.OnStart(this, new OnStartedEventArgs(filePath));
            await Task.Run(() =>
            {
                try
                {
                    var startTime = DateTime.Now;
                    var code = this._strategy.Recognize(filePath);
                    var millisecond = DateTime.Now.Subtract(startTime).TotalMilliseconds;
                    var threadId = Thread.CurrentThread.ManagedThreadId;
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
