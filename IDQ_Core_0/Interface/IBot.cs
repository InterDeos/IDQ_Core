using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDQ_Core_0.Interface
{
    public interface IBot
    {
        IMessageService LogMessageService { get; set; }
        IMessageService LogErrorMessageService { get; set; }
        bool Work { get; }

        void Execute(Action<IWebDriverManager> action);
    }
}
