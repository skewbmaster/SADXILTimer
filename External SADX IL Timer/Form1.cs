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

namespace External_SADX_IL_Timer
{
    public partial class ILTimer : Form
    {
        public static Process gameProc = new Process();
        public static int baseAddress;
        public static bool gameHooked = false;

        public static bool is3digit = false;

        public static bool isSettingsOpened = false;
        public static MenuItem startupUpdatesCheck;

        public ILTimer()
        {
            InitializeComponent();

            backgroundTiming.RunWorkerAsync();

            EventHandler settings = new EventHandler(Msettings);

            startupUpdatesCheck = new MenuItem("3 Digit MS", new EventHandler(MstartupUpdatesCheck));
            //startupUpdatesCheck.Checked = UserSettings.Default.UpdateCheckAtStartup;

            ContextMenu context = new ContextMenu();
            context.MenuItems.Add("Settings", settings);
            context.MenuItems.Add("-");
            context.MenuItems.Add(startupUpdatesCheck);
            ContextMenu = context;
        }

        private void backgroundTiming_DoWork(object sender, DoWorkEventArgs e)
        {
            int currentFrames, oldFrames;
            int currentSeconds = 0, currentMinutes = 0, currentGameState, currentCharacter;

            string tempFramesText = "00", tempSecondText = "00", tempMinuteText = "00";

            double offset3digits;

            Hook();

            currentFrames = ProcessMemory.ReadByte(gameProc, baseAddress + 0x0370EF35);
            oldFrames = currentFrames;

            resizeLabels();

            while (true)
            {
                while (gameHooked)
                {
                    currentFrames = ProcessMemory.ReadByte(gameProc, baseAddress + 0x0370EF35);
                    currentGameState = ProcessMemory.ReadByte(gameProc, baseAddress + 0x03722DE4);
                    currentCharacter = ProcessMemory.ReadByte(gameProc, baseAddress + 0x03722DC0);

                    offset3digits = is3digit == true ? 1.0 : 10.0;


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

                        if (currentFrames == 0)
                        {
                            if (is3digit) tempFramesText = "000";
                            else tempFramesText = "00";
                        }
                        else
                        {
                            tempFramesText = Math.Floor((60 - currentFrames) * ((1000.0 / offset3digits) / 60.0)).ToString();
                        }
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


                        if (currentFrames == 60)
                        {
                            if (is3digit) tempFramesText = "000";
                            else tempFramesText = "00";
                        }
                        else
                        {
                            tempFramesText = Math.Floor(currentFrames * ((1000.0 / offset3digits) / 60.0)).ToString();
                        }

                    }

                    tempSecondText = currentSeconds.ToString();
                    tempMinuteText = currentMinutes.ToString();

                    if (is3digit)
                    {
                        if (tempFramesText.Length == 1)
                        {
                            tempFramesText = "00" + tempFramesText;
                        }
                        else if (tempFramesText.Length == 2)
                        {
                            tempFramesText = "0" + tempFramesText;
                        }
                    }
                    else if (tempFramesText.Length == 1)
                    {
                        tempFramesText = "0" + tempFramesText;
                    }

                    if (tempSecondText.Length == 1)
                    {
                        tempSecondText = "0" + tempSecondText;
                    }
                    else if (currentSeconds < 0)
                    {
                        tempSecondText = "00";
                    }

                    framesLabel.Text = tempFramesText;
                    secondsLabel.Text = tempSecondText;
                    minutesLabel.Text = tempMinuteText;

                    oldFrames = currentFrames;

                    Thread.Sleep(5);
                }

                Hook();
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
                gameHooked = true;
                baseAddress = gameProc.MainModule.BaseAddress.ToInt32();
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
            int labelHeight = (int)(((ILTimer.ActiveForm.Size.Height - 39) / 2.0) - (49 / 2.0));

            int totalLength = minutesLabel.Size.Width + secondsLabel.Size.Width + framesLabel.Size.Width + 66;

            int labelStarts = (int)((ILTimer.ActiveForm.Size.Width / 2.0) - (totalLength / 2.0));

            minutesLabel.Location = new Point(labelStarts, labelHeight);
            separator1.Location = new Point(labelStarts + minutesLabel.Size.Width - 3, labelHeight);
            secondsLabel.Location = new Point(labelStarts + minutesLabel.Size.Width + 28, labelHeight);
            separator2.Location = new Point(labelStarts + minutesLabel.Size.Width + secondsLabel.Size.Width + 20, labelHeight);
            framesLabel.Location = new Point(labelStarts + minutesLabel.Size.Width + secondsLabel.Size.Width + 50, labelHeight);
        }

        private void ILTimer_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;

            // Ensure the Form remains in ratio (16 : 9).
            control.Size = new Size(control.Size.Width, (int)Math.Floor((control.Size.Width / 16.0) * 9));

            resizeLabels();
        }

        public void Msettings(object sender, EventArgs e)
        {
            if (isSettingsOpened)
            {
                return;
            }
        }

        public void MstartupUpdatesCheck(object sender, EventArgs e)
        {
            startupUpdatesCheck.Checked = startupUpdatesCheck.Checked == true ? false : true;
            is3digit = startupUpdatesCheck.Checked;

            if (is3digit)
            {
                framesLabel.Text = "000";
            } 
            else
            {
                framesLabel.Text = "00";
            }

            resizeLabels();
        }
    }
}
