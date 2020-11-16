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
    public static class GoodByeDPI
    {
        private static Process GBDPI_Process = null;
        private static string Arguments = string.Empty;

        public static Exception LastError { get; private set; } = new Exception();

        public static event PropertyChangedEventHandler RunStateChangedEvent;

        private static bool _IsAdmin = false;
        public static bool IsAdmin
        {
            get => _IsAdmin;
            set
            {
                if (_IsAdmin != value)
                {
                    _IsAdmin = value;
                    Dispose();
                }
            }
        }

        private static string _Path = "goodbyedpi.exe";
        public static string Path
        {
            get => _Path;
            set
            {
                try
                {
                    string _path = System.IO.Path.GetFullPath(value);
                    if (!System.IO.Path.GetExtension(_path).EndsWith("exe") || !File.Exists(_path))
                        throw new ArgumentException($"유효한 경로가 아닙니다.\n{Path}");

                    _Path = _path;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                Stop();
            }
        }

        private static bool _IsRun = false;
        public static bool IsRun
        {
            get => _IsRun && GBDPI_Process != null;
            private set
            {
                if (_IsRun != value)
                {
                    _IsRun = value;
                    RunStateChangedEvent?.Invoke(null, new PropertyChangedEventArgs("IsRun"));
                }
            }
        }

        private static void Setup()
        {
            if (IsRun)
            {
                LastError = new InvalidOperationException("이미 실행 중입니다.");
                return;
            }

            if (GBDPI_Process == null)
            {
                ProcessStartInfo startInfo;
                Process process;

                if (IsAdmin)
                {
                    startInfo = new ProcessStartInfo(Path)
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        //Verb = "runas",
                    };
                }
                else
                {
                    startInfo = new ProcessStartInfo(Path)
                    {
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                }

                process = new Process
                {
                    EnableRaisingEvents = true,
                    StartInfo = startInfo
                };

                GBDPI_Process = process;
                GBDPI_Process.Exited += ExitWatcher;
            }
            else if (Arguments != null)
            {
                GBDPI_Process.StartInfo.FileName = Path;
                GBDPI_Process.StartInfo.Arguments = Arguments;
            }
        }

        public static bool Start(IGoodByeDPIOption options)
        {
            if (options == null)
                LastError = new ArgumentException("유효한 설정값이 아닙니다.");

            var args = options.GetArgument();

            if (Arguments != args)
                Arguments = args;

            return Start();
        }

        public static bool Start()
        {
            if (IsRun)
            {
                LastError = new InvalidOperationException("이미 실행 중입니다.");
                return false;
            }

            if (Check.GoodByeDPIRunCheck())
            {
                LastError = new InvalidOperationException("프로세스가 이미 존재합니다.");
                return false;
            }

            if (Arguments == null)
                LastError = new ArgumentException("저장된 설정값이 없습니다.");

            Setup();

            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Stop();
                LastError = ex;
                return false;
            }

            return true;
        }

        private static bool Run()
        {
            try
            {
                GBDPI_Process.Start();
                IsRun = true;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex;
                Stop();
                return false;
            }
        }

        public static bool Stop()
        {
            if (!IsRun)
            {
                LastError = new InvalidOperationException("실행 중인 프로세스가 없습니다.");
                return false;
            }

            if (GBDPI_Process != null)
            {
                try
                {
                    GBDPI_Process.Kill();
                }
                catch
                {
                    Dispose();
                    return false;
                }
            }
            IsRun = false;
            return true;
        }
        
        private static void ExitWatcher(object sender, EventArgs args) => Stop();

        public static void Dispose()
        {
            if (GBDPI_Process != null)
            {
                try
                {
                    GBDPI_Process.Kill();
                }
                catch (Exception ex)
                {
                    LastError = ex;
                }
                
                try { GBDPI_Process.Exited -= ExitWatcher; } catch { }

                GBDPI_Process.Dispose();
                GBDPI_Process = null;
            }
            IsRun = false;
        }
    }
}
