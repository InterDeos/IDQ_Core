using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDQ_Core_0.Class;
using IDQ_Core_0.Class.MessageService;

namespace IDQ_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var web = new WebDriverManager();
            web.Action += Web_Action;
            web.ActionError += Web_ActionError;
            web.Create().GoToUrl("http://www.google.com");
            for (int i = 0; i < 100; i++)
            {
                web.GoToUrl("http://www.whoer.net").Sleep(300, 1000).GoToUrl("http://www.planetromeo.net").Sleep(300, 1000).GoToUrl("http://www.google.com");
            }
            
            Console.ReadKey();
        }

        private static void Web_ActionError(string obj)
        {
            WinFormMessageService.ShowError(obj);
        }

        private static void Web_Action(string obj)
        {
            new ConsoleMessageService().ShowMesssage(obj);
        }
    }
}
