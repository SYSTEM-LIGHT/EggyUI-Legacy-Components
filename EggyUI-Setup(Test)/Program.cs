using System.Diagnostics;
using System.Security.Principal;

namespace EggyUI_Setup
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string mode = args.Length > 0 ? args[0] : string.Empty;

            // 检查是否以管理员权限运行
            if (!IsRunAsAdmin())
            {
                // 重新以管理员权限启动
                var processInfo = new ProcessStartInfo
                {
                    FileName = Environment.ProcessPath,
                    UseShellExecute = true,
                    Verb = "runas" // 以管理员身份运行
                };
                try
                {
                    Process.Start(processInfo);
                }
                catch
                {
                    // 用户取消UAC
                    return;
                }
                return;
            }

            if (mode == "/?")
            {
                MessageBox.Show("参数用法：\n1. 直接运行程序以启动Eggy UI 3.0安装程序窗口。\n2. 使用参数 /q 以静默安装Eggy UI 3.0。\n3. 使用参数 /? 显示此帮助信息。", "Eggy UI 3.0安装程序", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            else if (mode == "/q")
            {
                int buildNumber = Environment.OSVersion.Version.Build;
                if ((buildNumber >= 10240) && (buildNumber < 21994))
                {
                    Win10EggyUISetup.Start();
                }
                else if (buildNumber >= 21994)
                {
                    Win11EggyUISetup.Start();
                }
                else
                {
                    MessageBox.Show("当前系统不支持安装Eggy UI 3.0，请将系统升级到Win10或Win11。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } 
            else
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Setup_Window());
            }
        }

        private static bool IsRunAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}