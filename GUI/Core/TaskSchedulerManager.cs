using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.Win32.TaskScheduler;

namespace GBDPIGUI.Core
{
    public class TaskSchedulerManager
    {
        public static bool Add()
        {
            Task tk = GetTask();

            if (tk == null)
            {
                try
                {
                    using var ts = TaskService.Instance;
                    var td = ts.NewTask();

                    td.RegistrationInfo.Author = "CarbonSIX";
                    td.RegistrationInfo.Description = "CensoringDPI Auto Run Task";

                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.AllowHardTerminate = false;

                    td.Settings.DisallowStartIfOnBatteries = false;
                    td.Settings.StopIfGoingOnBatteries = false;
                    td.Settings.RunOnlyIfIdle = false;
                    td.Settings.IdleSettings.RestartOnIdle = false;
                    td.Settings.IdleSettings.StopOnIdleEnd = false;
                    td.Settings.Hidden = true;
                    td.Settings.ExecutionTimeLimit = TimeSpan.Zero;

                    var logon = new LogonTrigger();
                    logon.Enabled = true;
                    logon.Delay = TimeSpan.FromSeconds(6);
                    logon.UserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    td.Triggers.Add(logon);

                    ExecAction execAction = new ExecAction(Assembly.GetExecutingAssembly().Location, "-ts");
                    td.Actions.Add(execAction);

                    tk = ts.RootFolder.RegisterTaskDefinition("CensoringDPI", td);
                }
                catch 
                {
                    return false;
                }
            }

            tk.Enabled = true;
            tk.Dispose();

            return true;
        }

        public static bool Remove()
        {
            Task tk = GetTask();

            if (tk != null)
            {
                using var ts = TaskService.Instance;
                tk.Stop();
                tk.Enabled = false;
                ts.RootFolder.DeleteTask(tk.Name);
                tk.Dispose();
                return true;
            }
            return false;
        }

        private static Task GetTask()
        {
            try
            {
                using var ts = TaskService.Instance;
                Task tk = ts.FindTask("CensoringDPI");
                return tk;
            }
            catch
            {
                return null;
            }
        }
    }
}
