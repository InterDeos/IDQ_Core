using System;
using IDQ_Core_0.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDQ_Core_0.Class.MessageService
{
    public class ConsoleMessageService : IMessageService
    {
        private ConsoleColor _defaultColor;
        private ConsoleColor _defaultBackgroundColor;
        private ConsoleColor _messageColor;
        private ConsoleColor _exclamationColor;
        private ConsoleColor _errorColor;

        public ConsoleColor DefaultColor { get => _defaultColor; set => _defaultColor = value; }
        public ConsoleColor MessageColor { get => _messageColor; set => _messageColor = value; }
        public ConsoleColor ExclamationColor { get => _exclamationColor; set => _exclamationColor = value; }
        public ConsoleColor ErrorColor { get => _errorColor; set => _errorColor = value; }
        public ConsoleColor DefaultBackgroundColor { get => _defaultBackgroundColor; set => _defaultBackgroundColor = value; }

        public ConsoleMessageService() : this(ConsoleColor.Gray, ConsoleColor.Black, ConsoleColor.Cyan, ConsoleColor.DarkYellow, ConsoleColor.DarkRed) { }
        public ConsoleMessageService(ConsoleColor defaultColor, ConsoleColor background, ConsoleColor message, ConsoleColor exclamation, ConsoleColor error)
        {
            DefaultColor = defaultColor;
            DefaultBackgroundColor = background;
            MessageColor = message;
            ExclamationColor = exclamation;
            ErrorColor = error;
        }

        
        public void ColorMessage(string message, ConsoleColor consoleColor, ConsoleColor background = ConsoleColor.Black)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.BackgroundColor = DefaultBackgroundColor;
            Console.ForegroundColor = DefaultColor;
        }

        public void TestMessages()
        {
            if(DefaultBackgroundColor == ConsoleColor.Black)
            {
                BlackWrite("Black\n", ConsoleColor.White);
            }
            else
            {
                BlackWrite("Black\n");
            }
            DarkBlueWrite("DarkBlueWrite\n");
            DarkGreenWrite("DarkGreenWrite\n");
            DarkCyanWrite("DarkCyanWrite\n");
            DarkRedWrite("DarkRedWrite\n");
            DarkMagentaWrite("DarkMagentaWrite\n");
            DarkYellowWrite("DarkYellowWrite\n");
            GrayWrite("GrayWrite\n");
            DarkGrayWrite("DarkGrayWrite\n");
            BlueWrite("BlueWrite\n");
            GreenWrite("GreenWrite\n");
            CyanWrite("CyanWrite\n");
            RedWrite("RedWrite\n");
            MagentaWrite("MagentaWrite\n");
            YellowWrite("YellowWrite\n");
            WhiteWrite("WhiteWrite\n");
        }

        public void BlackWrite(string message)
        {
            BlackWrite(message, DefaultBackgroundColor);
        }
        public void DarkBlueWrite(string message)
        {
            DarkBlueWrite(message, DefaultBackgroundColor);
        }
        public void DarkGreenWrite(string message)
        {
            DarkGreenWrite(message, DefaultBackgroundColor);
        }
        public void DarkCyanWrite(string message)
        {
            DarkCyanWrite(message, DefaultBackgroundColor);
        }
        public void DarkRedWrite(string message)
        {
            DarkRedWrite(message, DefaultBackgroundColor);
        }
        public void DarkMagentaWrite(string message)
        {
            DarkMagentaWrite(message, DefaultBackgroundColor);
        }
        public void DarkYellowWrite(string message)
        {
            DarkYellowWrite(message, DefaultBackgroundColor);
        }
        public void GrayWrite(string message)
        {
            GrayWrite(message, DefaultBackgroundColor);
        }
        public void DarkGrayWrite(string message)
        {
            DarkGrayWrite(message, DefaultBackgroundColor);
        }
        public void BlueWrite(string message)
        {
            BlueWrite(message, DefaultBackgroundColor);
        }
        public void GreenWrite(string message)
        {
            GreenWrite(message, DefaultBackgroundColor);
        }
        public void CyanWrite(string message)
        {
            CyanWrite(message, DefaultBackgroundColor);
        }
        public void RedWrite(string message)
        {
            RedWrite(message, DefaultBackgroundColor);
        }
        public void MagentaWrite(string message)
        {
            MagentaWrite(message, DefaultBackgroundColor);
        }
        public void YellowWrite(string message)
        {
            YellowWrite(message, DefaultBackgroundColor);
        }
        public void WhiteWrite(string message)
        {
            WhiteWrite(message, DefaultBackgroundColor);
        }

        public void BlackWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Black, background);
        }
        public void DarkBlueWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkBlue, background);
        }
        public void DarkGreenWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkGreen, background);
        }
        public void DarkCyanWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkCyan, background);
        }
        public void DarkRedWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkRed, background);
        }
        public void DarkMagentaWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkMagenta, background);
        }
        public void DarkYellowWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkYellow, background);
        }
        public void GrayWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Gray, background);
        }
        public void DarkGrayWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.DarkGray, background);
        }
        public void BlueWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Blue, background);
        }
        public void GreenWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Green, background);
        }
        public void CyanWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Cyan, background);
        }
        public void RedWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Red, background);
        }
        public void MagentaWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Magenta, background);
        }
        public void YellowWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.Yellow, background);
        }
        public void WhiteWrite(string message, ConsoleColor background = ConsoleColor.Black)
        {
            ColorMessage(message, ConsoleColor.White, background);
        }

        public void ShowError(string error)
        {
            ColorMessage("\n-------------------------------------------------------------\n", DefaultColor, DefaultBackgroundColor);
            GreenWrite(string.Format("[{0}]", DateTime.Now));
            ColorMessage("\n{Message}\n\n", ErrorColor, DefaultBackgroundColor);
            ColorMessage(error, ConsoleColor.White, DefaultBackgroundColor);
            ColorMessage("\n\n{Message}", ErrorColor, DefaultBackgroundColor);
            ColorMessage("\n-------------------------------------------------------------\n", DefaultColor, DefaultBackgroundColor);
        }
        public void ShowExclamation(string exclamation)
        {
            ColorMessage("\n-------------------------------------------------------------\n", DefaultColor, DefaultBackgroundColor);
            GreenWrite(string.Format("[{0}]", DateTime.Now));
            ColorMessage("\n{Message}\n\n", ExclamationColor, DefaultBackgroundColor);
            ColorMessage(exclamation, ConsoleColor.White, DefaultBackgroundColor);
            ColorMessage("\n\n{Message}", ExclamationColor, DefaultBackgroundColor);
            ColorMessage("\n-------------------------------------------------------------\n", DefaultColor, DefaultBackgroundColor);
        }
        public void ShowMesssage(string message)
        {
            ColorMessage("\n-------------------------------------------------------------\n", DefaultColor, DefaultBackgroundColor);
            GreenWrite(string.Format("[{0}]", DateTime.Now));
            ColorMessage("\n{Message}\n\n", MessageColor, DefaultBackgroundColor);
            ColorMessage(message, ConsoleColor.White, DefaultBackgroundColor);
            ColorMessage("\n\n{Message}", MessageColor, DefaultBackgroundColor);
            ColorMessage("\n-------------------------------------------------------------\n", DefaultColor, DefaultBackgroundColor);
        }
    }
}
