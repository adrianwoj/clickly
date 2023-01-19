using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Clickly.Core;

namespace Clickly
{
    public partial class MainWindow : Window
    {
        private readonly State State = new()
        {
            BraveryCape = new AutoClick(),
            AutoPickUp = new AutoClick
            {
                Keys = new List<KeyPress> {
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_Z }
                }
            },
            AutoSpace = new AutoClick {
                Keys = new List<KeyPress> {
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_SPACE }
                }
            },
            AutoBattlePass = new AutoClick
            {
                Keys = new List<KeyPress> {
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_RETURN },
                    new KeyPress { Shift = true, Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_1 },
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_B },
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_P },
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_RETURN },
                    new KeyPress { Code = (ushort)Keyboard.DirectXKeyStrokes.DIK_RETURN },
                },
                Delay = 12000,
                DelayVariation = 3000,
                InitialDelay = 2000
            }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BraveryCapeBtn_Click(object sender, RoutedEventArgs e)
        {
            var braveryCapeKey = Convert.ToInt32(BraveryCapeKeyTb.Text);

            State.BraveryCape.Keys = new List<KeyPress> { new KeyPress { Code = (ushort)(0x01 + braveryCapeKey) } };
            State.BraveryCape.StartAutoClick(BraveryCapeBtn, BraveryCapeBorder);
        }

        private void AutoPickUpBtn_Click(object sender, RoutedEventArgs e)
        {
            State.AutoPickUp.StartAutoClick(AutoPickUpBtn, AutoPickUpBorder);
        }

        private void AutoSpaceBtn_Click(object sender, RoutedEventArgs e)
        {
            State.AutoSpace.StartAutoClick(AutoSpaceBtn, AutoSpaceBorder);
        }

        private void AutoBattlePassBtn_Click(object sender, RoutedEventArgs e)
        {
            State.AutoBattlePass.StartAutoClick(AutoBattlePassBtn, AutoBattlePassBorder);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            State.BraveryCape.Enabled = false;
            State.AutoPickUp.Enabled = false;
            State.AutoSpace.Enabled = false;
            State.AutoBattlePass.Enabled = false;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
                e.Handled = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            State.BraveryCape.Delay = e.NewValue;

            if (BraveryCapeDelayLbl != null)
                BraveryCapeDelayLbl.Content = (e.NewValue / 1000).ToString("0.##") + " s";
        }
    }
}
