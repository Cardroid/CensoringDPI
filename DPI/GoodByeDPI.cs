using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using GoodByeDPI.NET.Interface;

namespace GoodByeDPI.NET
{
    public class GoodByeDPI
    {
        private GoodByeDPI()
        {
            this.IsRun = false;
        }

        private static GoodByeDPI Instence;

        public static GoodByeDPI GetInstence()
        {
            if (Instence == null)
                Instence = new GoodByeDPI();

            return Instence;
        }

        private void Init(string path)
        {
            GBDPI_Process = new Process
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

        private Process GBDPI_Process;

        public bool IsRun { get; private set; }
        public IGoodByeDPIOptions Options { get; private set; }

        public bool Start(IGoodByeDPIOptions options)
        {
            if (IsRun || ProcessRunCheck())
                return false;

            if (options == null)
            {
                if (this.Options == null)
                    return Start(options.Path, "");
                else
                    return Start(options.Path, Options.GetArgument());
            }
            else
            {
                this.Options = options;
                return Start(options.Path, options.GetArgument());
            }
        }

        public bool Start(string path, string arg)
        {
            if (IsRun || ProcessRunCheck())
                return false;

            if (Path.GetExtension(path.ToLower()).EndsWith("exe") && File.Exists(path))
            {
                GBDPI_Process = null;
                IsRun = false;
                return IsRun;
            }
            else
            {
                if (GBDPI_Process == null)
                    Init(path);
            }

            try
            {
                GBDPI_Process.StartInfo.Arguments = arg;
                GBDPI_Process.Start();
                IsRun = true;
            }
            catch
            {
                GBDPI_Process.Kill();
                IsRun = false;
            }

            return IsRun;
        }

        public bool Stop()
        {
            if (!IsRun)
                return false;
            IsRun = false;

            GBDPI_Process.Close();
            IsRun = false;
            return IsRun;
        }

        private bool ProcessRunCheck() => Process.GetProcessesByName("goodbyedpi").Length > 0 && Process.GetProcessesByName("CensoringDPI").Length > 0;
    }
}
