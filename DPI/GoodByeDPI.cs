using System;
using System.Diagnostics;
using System.Text;

namespace GoodByeDPI.NET
{
    public class GoodByeDPI
    {
        private GoodByeDPI()
        {
            this.IsRun = false;
        }

        private GoodByeDPI(string path) : this()
        {
            DPI_Process = new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = false,
                    StandardErrorEncoding = Encoding.UTF8,
                    StandardOutputEncoding = Encoding.UTF8,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    Verb = "runas"
                }
            };
        }

        private static GoodByeDPI Instence;

        public static GoodByeDPI GetInstence(string path)
        {
            if (Instence == null)
                Instence = new GoodByeDPI(path);

            return Instence;
        }

        public string Path => DPI_Process.StartInfo.FileName;

        private readonly Process DPI_Process;

        public bool IsRun { get; private set; }

        public GoodByeDPIOption Option { get; private set; }

        public bool Start(GoodByeDPIOption option = null)
        {
            if (IsRun || ProcessRunCheck())
                return false;

            if (option == null)
            {
                if (this.Option == null)
                    return Start("");
                else
                    return Start(Option.GetArgument());
            }
            else
            {
                this.Option = option;
                return Start(Option.GetArgument());
            }
        }

        private bool Start(string arg)
        {
            if (IsRun || ProcessRunCheck())
                return false;

            IsRun = true;

            DPI_Process.StartInfo.Arguments = arg;
            DPI_Process.Start();
            IsRun = true;
            return IsRun;
        }

        public bool Stop()
        {
            if (!IsRun)
                return false;
            IsRun = false;

            DPI_Process.Close();
            IsRun = false;
            return IsRun;
        }

        private bool ProcessRunCheck()
        {
            Process[] processes = Process.GetProcessesByName("goodbyedpi");
            return processes.Length > 0;
        }
    }
}
