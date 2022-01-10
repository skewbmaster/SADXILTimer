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
        private static bool isSpeedrunMod = false;
        
        private static Process gameProc = new Process();
        private static bool gameHooked = false;

        private static IntPtr moduleAddress;

        private static bool is3digit = false;

        private static bool isSettingsOpened = false;
        private static MenuItem digitSetting;

        private Timer backgroundTimer = new Timer()
        {
            Enabled = true,
            Interval = 5
        };

        public ILTimer()
        {
            InitializeComponent();

            FormClosing += (o, e) => Settings.SaveSettings();

            HandleCreated += (o, e) => InitialSetup();

            EventHandler settings = new EventHandler(Msettings);

            digitSetting = new MenuItem("3 Digit MS", new EventHandler(MdigitSetting));
            //startupUpdatesCheck.Checked = UserSettings.Default.UpdateCheckAtStartup;

            ContextMenu context = new ContextMenu();
            context.MenuItems.Add("Settings", settings);
            context.MenuItems.Add("-");
            context.MenuItems.Add(digitSetting);
            ContextMenu = context;
        }

        private int currentFrames, oldFrames;
        private int currentSeconds, currentMinutes, currentGameState, currentCharacter;
        private string tempFramesText = "00", tempSecondText = "00", tempMinuteText = "00";

        private void InitialSetup()
        {
            Settings.LoadSettings();

            Task hookTask = Task.Run(() => 
            { 
                Hook();

                currentFrames = gameProc.ReadByte(0x03B0EF35);
                oldFrames = currentFrames;

                backgroundTimer.Tick += backgroundTiming_DoWork;
            });

            ResizeLabels();
        }

        private void backgroundTiming_DoWork(object sender, EventArgs e)
        {
            if (gameHooked)
            {
                if (isSpeedrunMod)
                {
                    currentFrames = gameProc.ReadByte(moduleAddress + 0xD7AD);
                    currentSeconds = gameProc.ReadByte(moduleAddress + 0xD7AE);
                    currentMinutes = gameProc.ReadByte(moduleAddress + 0xD7AF);

                    if (currentFrames == 60)
                    {
                        tempFramesText = "000";
                    }
                    else
                    {
                        tempFramesText = ((int) Math.Floor(currentFrames * (1000.0 / 60.0))).ToString("D3");
                    }
                }
                else
                {
                    currentFrames = gameProc.ReadByte(0x03B0EF35);
                    currentGameState = gameProc.ReadByte(0x03B22DE4);
                    currentCharacter = gameProc.ReadByte(0x03B22DC0);

                    if (currentCharacter == 6) // If you're using Gamma
                    {
                        if (currentGameState == 3 || currentGameState == 4 || currentGameState == 6 ||
                            currentGameState == 7 || currentGameState == 21)
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

                        tempFramesText = ((int) Math.Floor((60 - currentFrames) * (1000.0 / 60.0))).ToString("D3");
                    }
                    else // Any other character that counts up
                    {
                        currentMinutes = gameProc.ReadByte(0x03B0EF48);
                        currentSeconds = gameProc.ReadByte(0x03B0F128);

                        if (currentGameState == 21)
                        {
                            currentMinutes = 0;
                            currentSeconds = 0;
                            currentFrames = 0;
                        }

                        tempFramesText = ((int) Math.Floor(currentFrames * (1000.0 / 60.0))).ToString("D3");
                    }
                }
                
                tempSecondText = currentSeconds > 0 ? currentSeconds.ToString("D2") : "00";
                tempMinuteText = currentMinutes.ToString("D2");

                if (!is3digit)
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

        private static bool Hook()
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
                gameHooked = true;

                moduleAddress = gameProc.GetModuleBaseAddress("sadx-speedrun-mod.dll");

                if (moduleAddress != IntPtr.Zero)
                {
                    isSpeedrunMod = true;
                    Console.WriteLine("Speedrun Mod Active");
                }
                
                return true;
            }
        }

        private static void GameProc_Exited(object sender, EventArgs e)
        {
            gameProc.Exited -= GameProc_Exited;
            gameHooked = false;
        }

        public void ResizeLabels()
        {
            if (WindowState == FormWindowState.Minimized) return;

            int labelHeight = (int)((Size.Height - 39) / 2.0 - 49 / 2.0);

            int labelStarts = (int)(Size.Width / 2.0 - timerLabel.Width / 2.0);

            timerLabel.Location = new Point(labelStarts, labelHeight);
        }

        private void ILTimer_Resize(object sender, EventArgs e)
        {
            ResizeLabels();
        }

        private static void Msettings(object sender, EventArgs e)
        {
            if (isSettingsOpened)
            {
                return;
            }

            new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Settings());
            }).Start();
        }

        private void MdigitSetting(object sender, EventArgs e)
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

            ResizeLabels();
        }
    }
}
