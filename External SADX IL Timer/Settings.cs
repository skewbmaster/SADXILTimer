using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserSettings = External_SADX_IL_Timer.Properties;

namespace External_SADX_IL_Timer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            Text = "Settings";
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MinimizeBox = false;

            backColorButton.Click += SelectBackColor;
            backColorButton2.BackColor = Program.MainForm.BackColor;

            fontColorButton.Click += SelectFontColor;
            fontColorButton2.BackColor = ILTimer.timerLabel.ForeColor;

            button1.Click += SelectFont;
        }

        static void SelectBackColor(object sender, EventArgs e)
        {
            ColorDialog BackColorD = new ColorDialog { Color = Program.MainForm.BackColor };
            if (BackColorD.ShowDialog() == DialogResult.OK)
            {
                Program.MainForm.BackColor = BackColorD.Color;
                backColorButton2.BackColor = BackColorD.Color;
            }
        }

        static void SelectFontColor(object sender, EventArgs e)
        {
            ColorDialog FontColorD = new ColorDialog { Color = ILTimer.timerLabel.ForeColor };
            if (FontColorD.ShowDialog() == DialogResult.OK)
            {
                ILTimer.timerLabel.ForeColor = FontColorD.Color;
                fontColorButton2.BackColor = FontColorD.Color;
            }
        }

        private void SelectFont(object sender, EventArgs e)
        {
            FontDialog FontSelectionD = new FontDialog { Font = ILTimer.timerLabel.Font };
            if (FontSelectionD.ShowDialog() == DialogResult.OK)
            {
                Program.MainForm.Invoke(new Action(() => 
                {
                    ILTimer.timerLabel.Font = FontSelectionD.Font;
                    Program.MainForm.resizeLabels();
                }));
            }
        }

        public static void LoadSettings()
        {
            Program.MainForm.Invoke(new Action(() =>
            {
                Program.MainForm.BackColor = UserSettings.Default.BackgroundColor;
                ILTimer.timerLabel.ForeColor = UserSettings.Default.FontColor;
                ILTimer.timerLabel.Font = UserSettings.Default.Font;
            }));
        }

        public static void SaveSettings()
        {
            UserSettings.Default.BackgroundColor = Program.MainForm.BackColor;
            UserSettings.Default.FontColor = ILTimer.timerLabel.ForeColor;
            UserSettings.Default.Font = ILTimer.timerLabel.Font;
            UserSettings.Default.Save();
        }
    }
}
