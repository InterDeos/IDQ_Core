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
            var log = new ConsoleMessageService();


            Console.ReadKey();
        }

        private static void Bot_ActionErrorEvent(string obj)
        {
            WinFormMessageService.ShowError(obj);
        }

        private static void Bot_ActionEvent(string obj)
        {
            new ConsoleMessageService().ShowMesssage(obj);
            if (obj == "Stop") { new ConsoleMessageService().ShowExclamation("Save"); }
        }

        private static void TestFor(WebDriverManager web)
        {
            for (int i = 0; i < 100; i++)
            {
                web.GoToUrl("http://www.whoer.net").Sleep(300, 1000).GoToUrl("http://www.planetromeo.net").Sleep(300, 1000).GoToUrl("http://www.google.com");
            }
        }
    }
}
