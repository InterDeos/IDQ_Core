using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDQ_Core_0.Interface
{
    public interface IWebDriverManager
    {
        IWebDriver Driver { get; }
        IWebElement WebElement { get; }
        List<IWebElement> WebElements { get; }
        
        string Title { get; }
        string PageSource { get; }
        string URL { get; }
        IOptions DriverOptions { get; }
        int TimeOutLikeSeconds { get; set; }
        bool Work { get; }

        event Action<string> ActionError;
        event Action<string> Action;

        IWebDriverManager Back();
        IWebDriverManager Click();
        IWebDriverManager Create();
        IWebDriverManager FindElement(string locator);
        IWebElement FindElementObj(string locator);
        IWebDriverManager FindElements(string locator);
        IEnumerable<IWebElement> FindElementsObj(string locator);
        IWebDriverManager Forward();
        IWebDriverManager GoToUrl(string url);
        bool IsElementPresent(string locator);
        bool Connection();
        IWebDriverManager Close();
        IWebDriverManager Quit();
        IWebDriverManager Refresh();
        IWebDriverManager SelectOption(string locator, string value);
        IWebDriverManager SendKeys(string keys);
        IWebDriverManager Sleep(int timeLikeMS);
        IWebDriverManager Sleep(int minTimeLikeMS, int maxTimeLikeMS);
        IWebDriverManager SwitchFrame(string name);
        IWebDriverManager SwitchToDefault();
    }
}
