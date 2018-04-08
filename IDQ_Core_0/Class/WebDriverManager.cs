using IDQ_Core_0.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
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
        private void StopAction()
        {
            Message("Stop");
            MessageError("Stop");
            Stop = true;
            Quit();
            
        }
        private void IfTryCatch(Action action)
        {
            if (!Stop)
            {
                if (Work)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception e)
                    {
                        switch (e.GetType().ToString())
                        {
                            case "OpenQA.Selenium.DriverServiceNotFoundException":
                                {
                                    MessageError("geckodriver for FireFox Not Found");
                                    break;
                                }
                            case "OpenQA.Selenium.NoSuchElementException":
                                {
                                    MessageError(string.Format("Element Not Found | TimeOut = {0} seconds", TimeOutLikeSeconds));
                                    break;
                                }
                            case "OpenQA.Selenium.WebDriverException":
                                {
                                    StopAction();
                                    MessageError(e.ToString());
                                    MessageError(e.Message);
                                    break;
                                }
                            case "System.NullReferenceException":
                                {
                                    StopAction();
                                    MessageError(e.ToString());
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
                else { MessageError("Driver was not created!"); }
            }
        }
        private bool IfTryCatch(Func<bool> func)
        {
            if (!Stop)
            {
                if (Work)
                {
                    try
                    {
                        return func();
                    }
                    catch (Exception e)
                    {
                        switch (e.GetType().ToString())
                        {
                            case "OpenQA.Selenium.DriverServiceNotFoundException":
                                {
                                    MessageError("geckodriver for FireFox Not Found");
                                    break;
                                }
                            case "OpenQA.Selenium.NoSuchElementException":
                                {
                                    MessageError(string.Format("Element Not Found | TimeOut = {0} seconds", TimeOutLikeSeconds));
                                    break;
                                }
                            case "OpenQA.Selenium.WebDriverException":
                                {
                                    StopAction();
                                    MessageError(e.ToString());
                                    MessageError(e.Message);
                                    break;
                                }
                            case "System.NullReferenceException":
                                {
                                    StopAction();
                                    MessageError(e.ToString());
                                    break;
                                }
                            default:
                                {
                                    MessageError(e.GetType().ToString());
                                    MessageError(e.Message);
                                    break;
                                }
                        }
                        return false;
                    }
                }
                else { MessageError("Driver was not created!"); return false; }
            }
            else { return false; }
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
                    Stop = false;
                }
                else
                {
                    Work = false;
                    Stop = false;
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
        public bool Stop { get; set; }

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
            if (!Work)
            {
                Message("Create Driver");
                Driver = GetDriver();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
            }else { MessageError("Driver was created!"); }
            return this;
        }
        public IWebDriverManager Close()
        {
            IfTryCatch(()=>
            {
                if (Driver.WindowHandles.Count > 1)
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
                    Stop = false;
                }
            });
            return this;
        }
        public IWebDriverManager Quit()
        {
            IfTryCatch(()=>
            {
                Message("Driver Quit");
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            });
            return this;
        }

        public bool Connection()
        {
            return IfTryCatch(()=>
            {
                if (IsElementPresent("p#errorShortDescText"))
                {
                    StopAction();
                    MessageError("Could not connect!");
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }
        public IWebDriverManager GoToUrl(string url)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Go to URL: {0}", url));
                Driver.Navigate().GoToUrl(url);
                Connection();
            });
            return this;
        }
        public IWebDriverManager Back()
        {
            IfTryCatch(()=>
            {
                Message("Back command");
                Driver.Navigate().Back();
                Connection();
            });
            return this;
        }
        public IWebDriverManager Refresh()
        {
            IfTryCatch(()=>
            {
                Message("Refresh command");
                Driver.Navigate().Refresh();
                Connection();
            });
            return this;
        }
        public IWebDriverManager Forward()
        {
            IfTryCatch(()=>
            {
                Message("Forward command");
                Driver.Navigate().Forward();
                Connection();
            });
            return this;
        }

        public bool IsElementPresent(string locator)
        {
            return IfTryCatch(()=>
            {
                if (!Stop)
                {
                    if (Work)
                    {
                        try
                        {
                            Message(string.Format("IS ElementPresent? {0}", locator));
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
                                case "OpenQA.Selenium.DriverServiceNotFoundException":
                                    {
                                        MessageError("geckodriver for FireFox Not Found");
                                        break;
                                    }
                                case "OpenQA.Selenium.NoSuchElementException":
                                    {
                                        Message(string.Format("IS ElementPresent = FALSE"));
                                        break;
                                    }
                                case "OpenQA.Selenium.WebDriverException":
                                    {
                                        StopAction();
                                        MessageError(e.ToString());
                                        MessageError(e.Message);
                                        break;
                                    }
                                case "System.NullReferenceException":
                                    {
                                        StopAction();
                                        MessageError(e.ToString());
                                        break;
                                    }
                                default:
                                    {
                                        MessageError(e.GetType().ToString());
                                        MessageError(e.Message);
                                        break;
                                    }
                            }
                            return false;
                        }
                    }
                    else { MessageError("Driver was not created!"); return false; }
                }
                else { return false; }
            });
        }

        public IWebDriverManager FindElement(string locator)
        {
            IfTryCatch(() =>
            {
                Message(string.Format("Find Element: {0}", locator));
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                WebElement = Driver.FindElement(byLocator(locator));
            });

            return this;
        }
        public IWebDriverManager FindElements(string locator)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Find Elements: {0}", locator));
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                WebElements = Driver.FindElements(byLocator(locator)).ToList();
            });
            return this;
        }
        public IWebElement FindElementObj(string locator)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format("Find Element as IWebElement: {0}", locator));
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                    return Driver.FindElement(byLocator(locator));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                    return null;
                }
            }
            else { CreatedMessageError(); return null; }
        }
        public IEnumerable<IWebElement> FindElementsObj(string locator)
        {
            if (Work)
            {
                try
                {
                    Message(string.Format("Find Element as IWebElement: {0}", locator));
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TimeOutLikeSeconds);
                    return Driver.FindElements(byLocator(locator));
                }
                catch (Exception e)
                {
                    MessageError(e.Message);
                    return null;
                }
            }
            else { CreatedMessageError(); return null; }
        }

        public IWebDriverManager Click()
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Click"));
                WebElement.Click();
            });
            return this;
        }
        public IWebDriverManager SendKeys(string keys)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Send: {0}", keys));
                WebElement.SendKeys(keys);
            });
            return this;
        }
        public IWebDriverManager Sleep(int timeLikeMS)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Sleep {0} milliseconds", timeLikeMS));
                Thread.Sleep(timeLikeMS);
            });
            return this;
        }
        public IWebDriverManager Sleep(int minTimeLikeMS, int maxTimeLikeMS)
        {
            Message(string.Format("Sleep From {0} to {1} milliseconds", minTimeLikeMS, maxTimeLikeMS));
            return Sleep(new Random().Next(minTimeLikeMS, maxTimeLikeMS));
        }

        public IWebDriverManager SelectOption(string locator, string value)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Select {0} in {1}", value, locator));
                new SelectElement(Driver.FindElement(byLocator(locator))).SelectByValue(value);
            });
            return this;
        }
        public IWebDriverManager SwitchFrame(string name)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Switch Frame {0}", name));
                Frames = Driver.FindElements(byLocator("frame")).ToList();
                IWebElement element = Frames.Find((x) => x.GetAttribute("name") == name);
                if (element != null)
                {
                    SwitchTo.Frame(element);
                }
                else
                {
                    MessageError("Frame not found");
                }
            });
            return this;
        }
        public IWebDriverManager SwitchToDefault()
        {
            IfTryCatch(()=>
            {
                Driver.SwitchTo().DefaultContent();
            });
            return this;
        }
        public List<string> GetWindows()
        {
            return Driver.WindowHandles.ToList();
        }
        public ITargetLocator SwitchTo
        {
            get => Work ? Driver.SwitchTo() : null;
        }

        public IWebDriverManager SwitcWindow(string window)
        {
            IfTryCatch(()=>
            {
                Message(string.Format("Switch window {0}", window));
                SwitchTo.Window(window);
            });
            return this;
        }
    }
}
