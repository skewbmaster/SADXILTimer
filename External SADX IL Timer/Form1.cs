using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace External_SADX_IL_Timer
{
    public partial class ILTimer : Form
    {
        public static Process gameProc = new Process();
        public static int baseAddress;
        public static bool gameHooked = false;

        public static bool is3digit = false;

        public static bool isSettingsOpened = false;
        public static MenuItem digitSetting;

        public Timer backgroundTimer = new Timer()
        {
            Enabled = true,
            Interval = 5
        };

        public ILTimer()
        {
            InitializeComponent();

            Activated += (o,e) => InitialSetup();

            EventHandler settings = new EventHandler(Msettings);

            digitSetting = new MenuItem("3 Digit MS", new EventHandler(MdigitSetting));
            //startupUpdatesCheck.Checked = UserSettings.Default.UpdateCheckAtStartup;

            ContextMenu context = new ContextMenu();
            context.MenuItems.Add("Settings", settings);
            context.MenuItems.Add("-");
            context.MenuItems.Add(digitSetting);
            ContextMenu = context;
        }


        int currentFrames, oldFrames;
        int currentSeconds = 0, currentMinutes = 0, currentGameState, currentCharacter;
        string tempFramesText = "00", tempSecondText = "00", tempMinuteText = "00";

        private void InitialSetup()
        {
            Task hookTask = Task.Run(() => 
            { 
                Hook();

                currentFrames = ProcessMemory.ReadByte(gameProc, baseAddress + 0x0370EF35);
                oldFrames = currentFrames;

                backgroundTimer.Tick += backgroundTiming_DoWork;
            });
            resizeLabels();
        }

        private void backgroundTiming_DoWork(object sender, EventArgs e)
        {
            if (gameHooked)
            {
                currentFrames = ProcessMemory.ReadByte(gameProc, baseAddress + 0x0370EF35);
                currentGameState = ProcessMemory.ReadByte(gameProc, baseAddress + 0x03722DE4);
                currentCharacter = ProcessMemory.ReadByte(gameProc, baseAddress + 0x03722DC0);

                if (currentCharacter == 6) // If you're using Gamma
                {
                    if (currentGameState == 3 || currentGameState == 4 || currentGameState == 6 || currentGameState == 7 || currentGameState == 21)
                    {
                        currentMinutes = 0;
                        currentSeconds = -1;
                        currentFrames = 60;
                        oldFrames = 60;
                    }

                    if (currentFrames > oldFrames)
                    {
                        currentSeconds += 1;
                    }

                    if (currentSeconds == 60)
                    {
                        currentMinutes += 1;
                        currentSeconds = 0;
                    }

                    tempFramesText = ((int)Math.Floor((60 - currentFrames) * (1000.0 / 60.0))).ToString("D3");
                }
                else // Any other character that counts up
                {
                    currentMinutes = ProcessMemory.ReadByte(gameProc, baseAddress + 0x0370EF48);
                    currentSeconds = ProcessMemory.ReadByte(gameProc, baseAddress + 0x0370F128);

                    if (currentGameState == 21)
                    {
                        currentMinutes = 0;
                        currentSeconds = 0;
                        currentFrames = 0;
                    }

                    tempFramesText = ((int)Math.Floor(currentFrames * (1000.0 / 60.0))).ToString("D3");
                }
                
                tempSecondText = currentSeconds > 0 ? currentSeconds.ToString("D2") : "00";
                tempMinuteText = currentMinutes.ToString("D2");

                if(!is3digit)
                {
                    tempFramesText = tempFramesText.Remove(2);
                }
                timerLabel.Text = $"{tempMinuteText} : {tempSecondText} . {tempFramesText}";

                oldFrames = currentFrames;
            }
            else
            {
                Task.Run(Hook);
            }
        }

        private bool Hook()
        {
            while (true)
            {
                try { gameProc = Process.GetProcessesByName("sonic").First(); }
                catch
                {
                    try { gameProc = Process.GetProcessesByName("Sonic Adventure DX").First(); }
                    catch
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                }

                gameProc.Exited += GameProc_Exited;
                gameProc.EnableRaisingEvents = true;
                baseAddress = gameProc.MainModule.BaseAddress.ToInt32();
                gameHooked = true;
                return true;
            }
        }

        private static void GameProc_Exited(object sender, EventArgs e)
        {
            gameProc.Exited -= GameProc_Exited;
            gameHooked = false;
        }

        private void resizeLabels()
        {
            if (WindowState == FormWindowState.Minimized) return;

            int labelHeight = (int)(((Size.Height - 39) / 2.0) - (49 / 2.0));

            int labelStarts = (int)((Size.Width / 2.0) - (timerLabel.Width / 2.0));

            timerLabel.Location = new Point(labelStarts, labelHeight);
        }

        private void ILTimer_Resize(object sender, EventArgs e)
        {
            resizeLabels();
        }

        public void Msettings(object sender, EventArgs e)
        {
            if (isSettingsOpened)
            {
                return;
            }
        }

        public void MdigitSetting(object sender, EventArgs e)
        {
            digitSetting.Checked = !digitSetting.Checked;
            is3digit = digitSetting.Checked;

            if (is3digit)
            {
                timerLabel.Text = "99 : 59 . 999";
            }
            else
            {
                timerLabel.Text = "99 : 59 . 99";
            }

            resizeLabels();
        }
    }
}
