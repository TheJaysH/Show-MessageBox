using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormDialogTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Count() > 0) ParseArgs(args);
            else Application.Run(new Form1());
        }

        private static string[] Params = new string[]
        {
            "--title"   ,"-t",
            "--message" ,"-m",
            "--icon"    ,"-i",
            "--buttons" ,"-b",
        };

        private static string[] AllowedButtons = new string[]
        {
            "OK",
            "OKCancel",
            "AbortRetryIgnore",
            "YesNoCancel",
            "YesNo",
            "RetryCancel",
        };

        private static string[] AllowedIcons = new string[]
        {
            "None",
            "Hand",
            "Stop",
            "Error",
            "Question",
            "Exclamation",
            "Warning",
            "Asterisk",
            "Information",
        };

        private static void ParseArgs(string[] args)
        {
            if ((args.Count() % 2) != 0)
            {
                throw new Exception("Please check args!");
            }

            var title = string.Empty;
            var message = string.Empty;

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.None;

            for (int i = 0; i < args.Count(); i++)
            {
                var key = args[i];
                var value = args[i+1];

                if (Params.Contains(key))
                {
                    switch (key)
                    {
                        case "--title":
                        case "-t":
                            title = value;
                            break;
                        case "--message":
                        case "-m":
                            message = value;
                            break;
                        case "--buttons":
                        case "-b":
                            if (AllowedButtons.Contains(value))
                                buttons = Form1.GetButtons(value);
                            else
                                throw new Exception($"MessageBoxButton not found for: '{value}'");
                            break;
                        case "--icon": 
                        case "-i":
                            if (AllowedIcons.Contains(value))
                                icon = Form1.GetIcon(value);
                            else
                                throw new Exception($"MessageBoxIcon not found for: '{value}'");                          
                            break;
                        default:
                            throw new NotImplementedException("Paramater not implimented!");
                    }
                }
                else
                {
                    throw new Exception($"Paramater not recognised: '{key}'");
                }

                i++;
            }

            Form1.ShowMessageBox(title, message, buttons, icon, true);
        }
    }
}
