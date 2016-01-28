using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha.Events
{
    public class OnStartedEventArgs : EventArgs
    {
        public string FilePath { get; private set; }

        public OnStartedEventArgs(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
