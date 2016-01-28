using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha.Events
{
    public class OnCompletedEventArgs : EventArgs
    {
        public string Code { get; private set; }

        public int ThreadId { get; private set; }

        public string FilePath { get; private set; }

        public double Milliseconds { get; private set; }

        public OnCompletedEventArgs(string code,double second , string filePath, int threadId)
        {
            this.Code = code;
            this.ThreadId = threadId;
            this.FilePath = filePath;
            this.Milliseconds = second;

        }
    }
}
