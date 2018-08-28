using System;
using System.Linq;
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

            if (args.Count() == 0)
                Application.Run(new Form1());
            else
                ParseArgs(args);
        }

        /// <summary>
        /// This is an array if the supported paramaters
        /// </summary>
        private static string[] Params = new string[]
        {
            "--title"   ,"-t",
            "--message" ,"-m",
            "--icon"    ,"-i",
            "--buttons" ,"-b",
        };

        /// <summary>
        /// This is an array of the buttons allowed by enum name
        /// </summary>
        private static string[] AllowedButtons = new string[]
        {
            "OK",
            "OKCancel",
            "AbortRetryIgnore",
            "YesNoCancel",
            "YesNo",
            "RetryCancel",
        };

        /// <summary>
        /// This is an array of the icons allowed by enum name
        /// </summary>
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


        /// <summary>
        /// Takes the args array and parses it.
        /// First checks to ensure its an even ammount.
        /// Then loop through in 2's getting the key and value
        /// if a key is found that does not match the AllowedParams array it will thow an exception
        /// </summary>
        /// <param name="args"></param>
        private static void ParseArgs(string[] args)
        {
            if ((args.Count() % 2) != 0)
            {
                throw new Exception("Arguments supplied do not have values assigned. Please check arguments.");
            }

            var title = string.Empty;
            var message = string.Empty;

            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.None;

            for (int i = 0; i < args.Count(); i++)
            {
                var key = args[i];
                var value = args[i + 1];

                if (Params.Contains(key))
                {
                    if (Params.Contains(value))
                    {
                        throw new Exception($"{key} requires a valid argument, `{value}` is not valid.");
                    }

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
                                buttons = Form1.GetEnum<MessageBoxButtons>(value);
                            else
                                throw new Exception($"MessageBoxButton not found for: '{value}'");
                            break;
                        case "--icon":
                        case "-i":
                            if (AllowedIcons.Contains(value))
                                icon = Form1.GetEnum<MessageBoxIcon>(value);
                            else
                                throw new Exception($"MessageBoxIcon not found for: '{value}'");
                            break;
                        default:
                            throw new NotImplementedException($"Paramater `{key}` not implimented!");
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
