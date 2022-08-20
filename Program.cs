using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
namespace AdminVSLauncher
{
    public static class Program
    {
        public static readonly string[] InstallLocations = new string[]
        {
            "C:\\Program Files\\Microsoft Visual Studio\\2022\\Professional\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\devenv.exe",

            "C:\\Program Files\\Microsoft Visual Studio\\2019\\Professional\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2019\\Enterprise\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2019\\Community\\Common7\\IDE\\devenv.exe",

            "C:\\Program Files\\Microsoft Visual Studio\\2017\\Professional\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2017\\Enterprise\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv.exe",

            "C:\\Program Files\\Microsoft Visual Studio\\2015\\Professional\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2015\\Enterprise\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2015\\Community\\Common7\\IDE\\devenv.exe",

            "C:\\Program Files\\Microsoft Visual Studio\\2013\\Professional\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2013\\Enterprise\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2013\\Community\\Common7\\IDE\\devenv.exe",

            "C:\\Program Files\\Microsoft Visual Studio\\2012\\Professional\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2012\\Enterprise\\Common7\\IDE\\devenv.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2012\\Community\\Common7\\IDE\\devenv.exe",
        };
        public static int Main()
        {
            try
            {
                string InstallLocation = null;

                for (int i = 0; i < InstallLocations.Length; i++)
                {
                    if (File.Exists(InstallLocations[i]))
                    {
                        InstallLocation = InstallLocations[i];
                        break;
                    }
                }

                if (InstallLocation is null)
                {
                    throw new Exception("Visual Studio Installation could not be located. This may be because visual studio has been installed to a non standard location or because it is not yet installed on this machine.");
                }

                ProcessStartInfo vsStartInfo = Process.GetCurrentProcess().StartInfo;
                vsStartInfo.FileName = InstallLocation;
                vsStartInfo.Verb = "runas";

                try
                {
                    Process.Start(vsStartInfo);
                }
                catch (System.ComponentModel.Win32Exception win32ex)
                {
                    if(win32ex.NativeErrorCode == 1223)
                    {

                    }
                    else
                    {
                        throw win32ex;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                try
                {
                    string stack = ex.StackTrace;
                    stack = stack.Replace("\n", "");
                    stack = stack.Replace("\t", "");
                    stack = stack.Replace("\r", "");
                    while (true)
                    {
                        string newStack = stack.Replace("  ", " ");
                        if (newStack == stack)
                        {
                            break;
                        }
                        else
                        {
                            stack = newStack;
                        }
                    }
                    if (stack.StartsWith(" "))
                    {
                        stack = stack.Substring(1, stack.Length - 1);
                    }
                    if (stack.EndsWith(" "))
                    {
                        stack = stack.Substring(0, stack.Length - 1);
                    }
                    string message = ex.Message;
                    message = message.Replace("\n", "");
                    message = message.Replace("\t", "");
                    message = message.Replace("\r", "");
                    while (message.EndsWith(".") || message.EndsWith(" "))
                    {
                        message = message.Substring(0, message.Length - 1);
                    }
                    while (message.StartsWith(" "))
                    {
                        message = message.Substring(1, message.Length - 1);
                    }
                    MessageBox.Show($"{ex.GetType().FullName}: {message} {stack}", $"{ex.GetType().Name} Thrown!");
                    return 1;
                }
                catch
                {
                    return 2;
                }
            }
        }
    }
}
