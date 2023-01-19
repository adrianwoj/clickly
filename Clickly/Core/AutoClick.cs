using Clickly.Constants;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace Clickly.Core
{
    public class KeyPress
    {
        public ushort Code { get; set; }
        public bool Shift { get; set; } = false;
    }

    public class AutoClick
    {
        public bool Enabled { get; set; } = false;
        public List<KeyPress> Keys { get; set; } = new List<KeyPress>();
        public double Delay { get; set; } = 100;
        public int DelayVariation { get; set; } = 50;
        public int InitialDelay { get; set; } = 0;

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

                if (InitialDelay != 0)
                    Thread.Sleep(InitialDelay);

                while (Enabled)
                {
                    foreach (var key in Keys)
                    {
                        if (key.Shift)
                            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, false, Keyboard.InputType.Keyboard);

                        Keyboard.SendKey(key.Code, false, Keyboard.InputType.Keyboard);
                        Thread.Sleep(random.Next(20, 50));
                        Keyboard.SendKey(key.Code, true, Keyboard.InputType.Keyboard);

                        if (key.Shift)
                            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_LSHIFT, true, Keyboard.InputType.Keyboard);
                    }

                    Thread.Sleep((int)Delay + random.Next(0, DelayVariation));
                }
            })).Start();
        }
    }
}
