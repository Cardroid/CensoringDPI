using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GoodByeDPIDotNet.Core;
using GoodByeDPIDotNet.Interface;

namespace GoodByeDPIDotNet
{
    public sealed class GoodByeDPI : INotifyPropertyChanged
    {
        private GoodByeDPI()
        {
        }

        private static GoodByeDPI Instence;

        public static GoodByeDPI GetInstence()
        {
            if (Instence == null)
                Instence = new GoodByeDPI();
            return Instence;
        }

        private void Setup(string path, bool isAdmin)
        {
            ProcessStartInfo startInfo;
            Process process;

            if (isAdmin)
                startInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = false,
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

            process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };

            Dispose();
            GBDPI_Process = process;

            GBDPI_Process.Exited += ExitWatcher;
        }

        private Process GBDPI_Process;

        private string _Path;
        public string Path
        {
            get => _Path;
            private set
            {
                _Path = value;
                OnPropertyChanged("Path");
            }
        }

        private bool _IsAdmin;
        public bool IsAdmin
        {
            get => _IsAdmin;
            private set
            {
                _IsAdmin = value;
                OnPropertyChanged("IsAdmin");
            }
        }

        private bool _IsRun = false;
        public bool IsRun
        {
            get => _IsRun && GBDPI_Process != null;
            private set
            {
                if (GBDPI_Process == null)
                    _IsRun = false;
                else
                    _IsRun = value;
                OnPropertyChanged("IsRun");
            }
        }

        private void ExitWatcher(object sender, EventArgs args) => Dispose();

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public IGoodByeDPIOptions Options { get; private set; }

        public GenericResult<string> Start() => Start(Options);

        public GenericResult<string> Start(IGoodByeDPIOptions options)
        {
            if (IsRun)
                return new GenericResult<string>(false, "이미 실행 중입니다.");

            if (Checker.GoodByeDPIRunCheck())
                return new GenericResult<string>(false, "프로세스가 이미 존재합니다.");

            if (options == null)
                return new GenericResult<string>(false, "유효한 설정값이 아닙니다.");

            if (Options != options)
                Options = options;

            return Start(options.Path, options.GetArgument(), options.IsAdmin);
        }

        public GenericResult<string> Start(string path, string arg, bool isAdmin)
        {
            if (IsRun)
                return new GenericResult<string>(false, "이미 실행 중입니다.");

            if (Checker.GoodByeDPIRunCheck())
                return new GenericResult<string>(false, "프로세스가 이미 존재합니다.");

            try
            {
                path = System.IO.Path.GetFullPath(path);
                if (!System.IO.Path.GetExtension(path).EndsWith("exe") || !File.Exists(path))
                    throw new ArgumentException();
            }
            catch
            {
                Dispose();
                return new GenericResult<string>(false, $"유효한 경로가 아닙니다.\n{path}");
            }

            if (Path != path)
            {
                Dispose();
                Path = path;
            }

            if (IsAdmin != isAdmin)
            {
                Dispose();
                IsAdmin = isAdmin;
            }

            if (GBDPI_Process == null)
                Setup(Path, IsAdmin);

            try
            {
                if (!string.IsNullOrWhiteSpace(arg))
                    GBDPI_Process.StartInfo.Arguments = arg;
                else
                    arg = "Null";
                Run();
            }
            catch (Win32Exception e)
            {
                Dispose();
                return new GenericResult<string>(false, $"인수: {arg}\n{e.Message}");
            }
            catch (Exception e)
            {
                Dispose();
                return new GenericResult<string>(false, $"인수: {arg}\n예외 발생: {e}");
            }

            return new GenericResult<string>(true, $"프로세스를 시작하였습니다.\n인수: {arg}");
        }

        public GenericResult<string> Stop()
        {
            if (!IsRun)
                return new GenericResult<string>(false, "실행 중인 프로세스가 없습니다.");

            Dispose();
            return new GenericResult<string>(true, "프로세스를 종료했습니다.");
        }

        private void Run()
        {
            try
            {
                GBDPI_Process.Start();
                IsRun = true;
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public void Kill()
        {
            if (GBDPI_Process != null)
                try { GBDPI_Process.Kill(); } catch { }
            IsRun = false;
        }

        public void Dispose()
        {
            Kill();

            if (GBDPI_Process != null)
                GBDPI_Process.Dispose();
            GBDPI_Process = null;
        }
    }
}
