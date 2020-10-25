using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

using GoodByeDPIDotNet.Core;
using GoodByeDPIDotNet.Interface;

namespace GoodByeDPIDotNet
{
    public class GoodByeDPI
    {
        private GoodByeDPI()
        {
            this.IsRun = GoodByeDPIManager.GoodByeDPIRunCheck();
        }

        private static GoodByeDPI Instence;

        public static GoodByeDPI GetInstence()
        {
            if (Instence == null)
                Instence = new GoodByeDPI();
            return Instence;
        }

        private void Init(string path, bool isAdmin)
        {
            ProcessStartInfo startInfo;

            if (isAdmin)
                startInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = false,
                    StandardErrorEncoding = Encoding.UTF8,
                    StandardOutputEncoding = Encoding.UTF8,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    Verb = "runas"
                };
            else
                startInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

            GBDPI_Process = new Process
            {
                StartInfo = startInfo
            };
        }

        private Process GBDPI_Process;

        public event EventHandler<GoodByeDPIEventArgs> GoodByeDPIEvent;
        public bool IsRun { get; private set; }
        public IGoodByeDPIOptions Options { get; private set; }

        public GenericResult<string> Start(IGoodByeDPIOptions options)
        {
            if (IsRun || GoodByeDPIManager.GoodByeDPIRunCheck())
                return new GenericResult<string>(false, "이미 실행 중입니다.");

            if (options == null)
            {
                if (this.Options == null)
                    return Start(options.Path, "", options.IsAdmin);
                else
                    return Start(options.Path, Options.GetArgument(), options.IsAdmin);
            }
            else
            {
                this.Options = options;
                return Start(options.Path, options.GetArgument(), options.IsAdmin);
            }
        }

        public GenericResult<string> Start(string path, string arg, bool isAdmin)
        {
            if (IsRun || GoodByeDPIManager.GoodByeDPIRunCheck())
                return new GenericResult<string>(false, "이미 실행 중입니다.");

            if (!Path.GetExtension(path.ToLower()).EndsWith("exe") || !File.Exists(path))
            {
                GBDPI_Process = null;
                IsRun = GoodByeDPIManager.GoodByeDPIRunCheck();
                return new GenericResult<string>(IsRun, $"유효한 경로가 아닙니다.\n{path}");
            }
            else
            {
                if (GBDPI_Process == null)
                    Init(path, isAdmin);
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(arg))
                    GBDPI_Process.StartInfo.Arguments = arg;
                else
                    arg = "Null";
                GBDPI_Process.Start();
            }
            catch (Win32Exception e)
            {
                Kill();
                return new GenericResult<string>(IsRun, $"인수: {arg}\n{e.Message}");
            }
            catch (Exception e)
            {
                Kill();
                return new GenericResult<string>(IsRun, $"인수: {arg}\n예외 발생: {e}");
            }

            IsRun = GoodByeDPIManager.GoodByeDPIRunCheck();
            return new GenericResult<string>(IsRun, $"프로세스를 시작하였습니다.\n인수: {arg}");
        }

        public GenericResult<string> Stop()
        {
            if (!IsRun)
                return new GenericResult<string>(false, "실행 중인 프로세스가 없습니다.");

            try
            {
                if (GBDPI_Process != null)
                    GBDPI_Process.Kill();
                IsRun = false;
            }
            catch (Exception e)
            {
                Kill();
                return new GenericResult<string>(false, $"프로세스 종료를 실패했습니다.\n예외 발생: {e}");
            }
     
            return new GenericResult<string>(true, "프로세스를 종료했습니다.");
        }

        private void Kill()
        {
            if (GoodByeDPIManager.GoodByeDPIRunCheck())
                try { GBDPI_Process.Kill(); } catch { }
            IsRun = false;
        }

        private async void ProcessWatcher()
        {
            while (IsRun)
            {

            }
            GoodByeDPIEvent?.Invoke();
        }
    }
}
