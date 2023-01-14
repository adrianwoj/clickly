using Clickly.Constants;
using System;
using System.Threading;
using System.Windows.Controls;

namespace Clickly.Core
{
    public class AutoClick
    {
        public bool Enabled { get; set; } = false;
        public ushort Key { get; set; }
        public double Delay { get; set; } = 100;

        public void StartAutoClick(Button button, Border border)
        {
            SetButtonAndBorder(button, border);
            StartAutoClickLoop();
        }

        private void SetButtonAndBorder(Button button, Border border)
        {
            if (Enabled)
            {
                Enabled = false;
                button.Content = "Start";
                border.Background = Burshes.DarkGrayBrush;
                return;
            }

            Enabled = true;
            button.Content = "Stop";
            border.Background = Burshes.GreenBrush;
        }

        private void StartAutoClickLoop()
        {
            var random = new Random();

            new Thread(new ThreadStart(() => {
                while (Enabled)
                {
                    Keyboard.SendKey(Key, false, Keyboard.InputType.Keyboard);
                    Thread.Sleep(random.Next(20, 50));
                    Keyboard.SendKey(Key, true, Keyboard.InputType.Keyboard);

                    Thread.Sleep((int)Delay + random.Next(-50, 50));
                }
            })).Start();
        }
    }
}
