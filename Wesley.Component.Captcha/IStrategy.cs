using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha
{
    /// <summary>
    /// 验证码识别策略接口
    /// </summary>
    public interface IStrategy
    {
        Account Account { set; }

        string Recognize(string filePath);
    }
}
