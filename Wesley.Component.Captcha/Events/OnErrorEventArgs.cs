using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha.Events
{
    public class OnErrorEventArgs : EventArgs
    {
        public string FilePath { get; private set; }

        public Exception Exception { get; private set; }

        public OnErrorEventArgs(string filePath, Exception exception)
        {
            this.Exception = exception;
            this.FilePath = filePath;
        }
    }
}
