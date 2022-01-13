using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace External_SADX_IL_Timer
{
    public partial class ILTimer : Form
    {
        private static bool is3digit = false;
        private static bool isSettingsOpened = false;
        private static MenuItem digitSetting;
        
        private static Process gameProc = new Process();
        private static bool gameHooked = false;
        private static bool hooking = false;

        private static bool isSpeedrunMod = false;
        private static IntPtr speedrunModMemory;
        private static IntPtr gammaFramesAddress;
        private static IntPtr gammaSecondsAddress;
        private static IntPtr gammaMinutesAddress;

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

            Task.Run(() => 
            { 
                Hook();

                currentFrames = gameProc.ReadByte(0x03B0EF35);
                oldFrames = currentFrames;

                backgroundTimer.Tick += backgroundTiming_DoWork;
            });

            ResizeLabels();
        }

        #region TimingLogic
        private void backgroundTiming_DoWork(object sender, EventArgs e)
        {
            if (gameHooked)
            {
                if (isSpeedrunMod)
                {
                    try
                    {
                        currentFrames = gameProc.ReadByte(gammaFramesAddress);
                        currentSeconds = gameProc.ReadByte(gammaSecondsAddress);
                        currentMinutes = gameProc.ReadByte(gammaMinutesAddress);
                    }
                    catch
                    {
                        return;
                    }

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
                    try
                    {
                        currentFrames = gameProc.ReadByte(0x03B0EF35);
                        currentGameState = gameProc.ReadByte(0x03B22DE4);
                        currentCharacter = gameProc.ReadByte(0x03B22DC0);
                    }
                    catch
                    {
                        return;
                    }

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
                            currentSeconds++;
                        }

                        if (currentSeconds >= 60)
                        {
                            currentMinutes++;
                            currentSeconds -= 60;
                        }

                        if (currentFrames == 0)
                        {
                            tempFramesText = "000";
                        }
                        else
                        {
                            tempFramesText = ((int) Math.Floor((60 - currentFrames) * (1000.0 / 60.0))).ToString("D3");
                        }
                    }
                    
                    else // Any other character that counts up
                    {
                        try
                        {
                            currentMinutes = gameProc.ReadByte(0x03B0EF48);
                            currentSeconds = gameProc.ReadByte(0x03B0F128);
                        }
                        catch
                        {
                            return;
                        }
                        
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
                if (!hooking)
                {
                    Task.Run(Hook);
                    hooking = true;
                }
            }
        }
        #endregion
        
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
                hooking = false;
                

                if (gameProc.GetModuleBaseAddress("sadx-speedrun-mod.dll") != IntPtr.Zero)
                {
                    isSpeedrunMod = true;
                    Console.WriteLine("Speedrun Mod Active");

                    speedrunModMemory = new IntPtr(gameProc.ReadInt32(0x426028));

                    gammaFramesAddress = new IntPtr(gameProc.ReadInt32(speedrunModMemory));
                    gammaSecondsAddress = new IntPtr(gameProc.ReadInt32(speedrunModMemory + 0x4));
                    gammaMinutesAddress = new IntPtr(gameProc.ReadInt32(speedrunModMemory + 0x8));
                    
                    return true;
                }

                isSpeedrunMod = false;
                return true;
            }
        }

        private static void GameProc_Exited(object sender, EventArgs e)
        {
            gameProc.Exited -= GameProc_Exited;
            gameHooked = false;
        }

        #region FormsShit
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
        #endregion
    }
}
