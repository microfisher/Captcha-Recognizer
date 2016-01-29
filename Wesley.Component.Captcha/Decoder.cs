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
    /// <summary>
    /// 验证码识别类
    /// </summary>
    public class Decoder : IDecoder
    {

        public event EventHandler<OnErrorEventArgs> OnError;
        public event EventHandler<OnStartedEventArgs> OnStart;
        public event EventHandler<OnCompletedEventArgs> OnCompleted;

        private readonly Account _account;
        private readonly Platform _platform;
        private readonly IStrategy _strategy;

        /// <summary>
        /// 使用平台及策略类中设置的账户信息初始化
        /// </summary>
        /// <param name="platform">打码平台</param>
        public Decoder(Platform platform)
        {
            this._platform = platform;
            this._strategy = GetStrategy();
        }

        /// <summary>
        /// 使用平台及自定义账户信息初始化
        /// </summary>
        /// <param name="platform">打码平台</param>
        /// <param name="account">账户信息</param>
        public Decoder(Platform platform,Account account)
        {
            this._account = account;
            this._platform = platform;
            this._strategy = GetStrategy();
        }

        /// <summary>
        /// 反射方式获取策略类
        /// </summary>
        /// <returns>策略实体</returns>
        private IStrategy GetStrategy()
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(type => typeof(IStrategy).IsAssignableFrom(type) && !type.IsAbstract && type.FullName.ToLower().Contains(this._platform.ToString().ToLower()));
            return Activator.CreateInstance(assembly, this._account) as IStrategy;
        }

        /// <summary>
        /// 调用策略类中的识别方法
        /// </summary>
        /// <param name="filePath">验证码图片路径</param>
        /// <returns>异步任务</returns>
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
