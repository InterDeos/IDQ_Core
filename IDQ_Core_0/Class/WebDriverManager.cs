using IDQ_Core_0.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IDQ_Core_0.Class
{
    public enum Selector
    {
        CssSelector = 0,
        XPath = 1
    }

    public class WebDriverManager : IWebDriverManager
    {
        private Func<string, By> byLocator;
        private int _timeOutLikeSeconds;
        private IWebDriver _driver;
        private FirefoxDriverService firefoxDriverService;

        private Func<string, By> GetSelector(Selector selector)
        {
            Func<string, By> _locator = null;
            switch (selector)
            {
                case Selector.CssSelector:
                    {
                        _locator = (locator) => By.CssSelector(locator);
                        break;
                    }
                case Selector.XPath:
                    {
                        _locator = (locator) => By.XPath(locator);
                        break;
                    }
            }
            return _locator;
        }
        private IWebDriver GetDriver()
        {
            firefoxDriverService = FirefoxDriverService.CreateDefaultService();
            firefoxDriverService.HideCommandPromptWindow = true;
            return new FirefoxDriver();
        }
        private void CreatedMessageError()
        {
            MessageError("Driver was not created!");
        }

        private void Message(string message)
        {
            Action?.Invoke(message);
        }
        private void MessageError(string message)
        {
            ActionError?.Invoke(message);
        }


        public IWebDriver Driver
        {
            get { return Work ? _driver : null; }
            private set
            {
                if(value != null)
                {
                    _driver = value;
                    Work = true;
                }
                else
                {
                    Work = false;
                }
            }
        }
        public IWebElement WebElement { get; private set; }
        public List<IWebElement> WebElements { get; private set; }
        public List<IWebElement> Frames { get; private set; }
        public IOptions DriverOptions { get { return Work ? Driver.Manage() : null; } }

        public int TimeOutLikeSeconds
        {
            get => _timeOutLikeSeconds;
            set
            {
                if (value > 0 && value < 180)
                {
                    _timeOutLikeSeconds = value;
                }
                else { _timeOutLikeSeconds = 30; }

            }
        }

        public string Title { get { return Work ? _driver.Title : ""; } }
        public string PageSource { get { return Work ? _driver.PageSource : ""; } }
        public string URL { get { return Work ? _driver.Url : ""; } }

        public bool Work { get; private set; }

        public WebDriverManager() : this(false) { }
        public WebDriverManager(bool onCreateStart) : this(onCreateStart, Selector.CssSelector) { }
        public WebDriverManager(bool onCreateStart = false, Selector selector = Selector.CssSelector, int timeOutLikeSeconds = 30)
        {
            byLocator = GetSelector(selector);
            TimeOutLikeSeconds = timeOutLikeSeconds;

            Driver = onCreateStart ? GetDriver() : null;

        }
        ~WebDriverManager()
        {
            if (Work)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }

        public event Action<string> Action;
        public event Action<string> ActionError;

        public IWebDriverManager Create()
        {
            if(!Work)
            {
                try
                {
                    Message("Create Driver");
                    Driver = GetDriver();
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                }catch(Exception e)
                {
                    switch (e.GetType().ToString())
                    {
                        case "OpenQA.Selenium.DriverServiceNotFoundException":
                            {
                                MessageError("geckodriver for FireFox Not Found");
                                break;
                            }
                        default:
                            {
                                MessageError(e.GetType().ToString());
                                MessageError(e.Message);
                                break;
                            }
                    }
                }
            }
            else { MessageError("Driver was created!"); }
            return this;
        }
        public IWebDriverManager Close()
        {
            if (Work)
            {
                if(Driver.WindowHandles.Count>1)
                {
                    Message("Close Window");
                    Driver.Close();
                }
                else
                {
                    Message("Driver Quit");
                    Driver.Close();
                    Driver.Dispose();
                    Driver = null;
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager Quit()
        {
            if (Work)
            {
                Message("Driver Quit");
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
            else { CreatedMessageError(); }
            return this;
        }

        public bool Connection()
        {
            if(Work)
            {
                if (IsElementPresent("p#errorShortDescText"))
                {
                    MessageError("Could not connect!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else { CreatedMessageError(); return false; }
        }
        public IWebDriverManager GoToUrl(string url)
        {
            if (Work)
            {
                Message(string.Format("Go to URL: {0}", url));
                Driver.Navigate().GoToUrl(url);
                Connection();
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager Back()
        {
            if (Work)
            {
                Message("Back command");
                Driver.Navigate().Back();
                Connection();
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager Refresh()
        {
            if (Work)
            {
                Message("Refresh command");
                Driver.Navigate().Refresh();
                Connection();
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager Forward()
        {
            if (Work)
            {
                Message("Forward command");
                Driver.Navigate().Forward();
                Connection();
            }
            else { CreatedMessageError(); }
            return this;
        }

        public bool IsElementPresent(string locator)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format("IS ElementPresent?"));
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                    Driver.FindElement(byLocator(locator));
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                    Message(string.Format("IS ElementPresent = TRUE"));
                    return true;

                }
                catch (Exception e)
                {
                    switch (e.GetType().ToString())
                    {
                        case "OpenQA.Selenium.NoSuchElementException":
                            {
                                Message(string.Format("IS ElementPresent = FALSE"));
                                break;
                            }
                        default:
                            {
                                MessageError(e.GetType().ToString());
                                MessageError(e.Message);
                                break;
                            }
                    }
                    
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                    return false;
                }
            }
            else { CreatedMessageError(); return false; }
        }

        public IWebDriverManager FindElement(string locator)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format("Find Element: {0}", locator));
                    WebElement = Driver.FindElement(byLocator(locator));
                }
                catch (Exception e)
                {
                    MessageError(e.GetType().ToString());
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager FindElements(string locator)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format("Find Elements: {0}", locator));
                    WebElements = Driver.FindElements(byLocator(locator)).ToList();
                }
                catch (Exception e)
                {
                    MessageError(e.GetType().ToString());
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebElement FindElementObj(string locator)
        {
            if (Work)
            {
                throw new NotImplementedException();
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); return null; }
        }
        public IEnumerable<IWebElement> FindElementsObj(string locator)
        {
            if (Work)
            {
                throw new NotImplementedException();
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); return null; }
        }

        public IWebDriverManager Click()
        {
            if (Work)
            {
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager SendKeys(string keys)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager Sleep(int timeLikeMS)
        {
            if (Work)
            {
                Message(string.Format("Sleep {0} milliseconds", timeLikeMS));
                Thread.Sleep(timeLikeMS);
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager Sleep(int minTimeLikeMS, int maxTimeLikeMS)
        {
            if (Work)
            {
                Message(string.Format("Sleep From {0} to {1} milliseconds", minTimeLikeMS, maxTimeLikeMS));
                return Sleep(new Random().Next(minTimeLikeMS, maxTimeLikeMS));
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager SelectOption(string locator, string value)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager SwitchFrame(string name)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }
        public IWebDriverManager SwitchToDefault()
        {
            if (Work)
            {
                try
                {
                    Message(string.Format(""));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                }
            }
            else { CreatedMessageError(); }
            return this;
        }







    }
}
