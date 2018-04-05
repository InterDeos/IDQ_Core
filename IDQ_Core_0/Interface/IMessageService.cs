using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDQ_Core_0.Interface
{
    public interface IMessageService
    {
        void ShowError(string error);
        void ShowExclamation(string exclamation);
        void ShowMesssage(string message);
    }
}
