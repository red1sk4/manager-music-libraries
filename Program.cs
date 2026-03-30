using System;
using System.Windows.Forms;
using MusicLibrary.Forms;
using MusicLibrary.Data;

namespace MusicLibrary
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Storage.Load();
            Application.Run(new LoginForm());
        }
    }
}
