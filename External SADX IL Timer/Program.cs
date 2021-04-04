using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace External_SADX_IL_Timer
{
    static class Program
    {
        public static ILTimer MainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new ILTimer();
            Application.Run(MainForm);
        }
    }
}
