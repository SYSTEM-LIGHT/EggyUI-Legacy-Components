using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 这是Eggy UI 3.0安装程序的代码。
 * 作者：冷落的小情绪
 * 作者B站主页：https://space.bilibili.com/3546772339165612
 * 作者蛋仔派对ID：冷落的小情绪
 * 欢迎来到Eggy UI官方QQ群：882583677 */

namespace EggyUI_Setup
{
    internal class Setup
    {
        public static string SourcePath = "C:\\EggyResource"; // 资源路径

        public static void ExpandFile()
        {
            // 解压文件并等待完成
            var process = Process.Start("7z.exe", ("x .\\EggyResource.7z -o\"" + SourcePath + "\" -pEggyUI2025 -aoa"));
            process?.WaitForExit();
        }

        public static void Install_Font()
        {
            // 安装字体并等待完成
            var process = Process.Start(SourcePath + "\\Font\\Register.cmd");
            process?.WaitForExit();
        }

        public static void Install_Start11()
        {
            // 安装Start11并等待完成
            var process = Process.Start(SourcePath + "\\StartAllBack\\Start11.exe", "/S");
            process?.WaitForExit();
        }

        public static void Install_StartAllBack()
        {
            // 安装StartAllBack并等待完成
            var process = Process.Start(SourcePath + "\\StartAllBack\\StartAllBack.exe", "/S");
            process?.WaitForExit();
        }

        public static void Change_StartAllBack_Icon()
        {
            // 更改StartAllBack开始图标
            RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\StartIsBack");
            key.SetValue("OrbBitmap", "C:\\EggyResource\\start.png");
            key.Close();
        }

        public static void Change_Start11_Icon()
        {
            // 更改Start11开始图标
            RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\Stardock\\Start8\\Start8.ini\\Start8");
            key.SetValue("Image", "C:\\EggyResource\\start_start11.png");
            key.Close();
        }

        public static void Change_Theme()
        {
            // 更改主题并等待完成
            var process = Process.Start(SourcePath + "\\Themes\\theme.cmd");
            process?.WaitForExit();
        }

        public static void Create_Rainmeter_Task()
        {
            // 创建Rainmeter计划任务并等待完成
            var process = Process.Start(SourcePath + "\\Rainmeter\\task.cmd");
            process?.WaitForExit();
        }

        public static void Create_Link()
        {
            // 创建自制应用链接并等待完成
            var process = Process.Start(SourcePath + "\\Experiment\\link.cmd");
            process?.WaitForExit();
        }

        public static void Install_FolderBackground()
        {
            // 安装文件夹背景并等待完成
            var process = Process.Start(SourcePath + "\\FolderBackground\\Register_oobe.cmd");
            process?.WaitForExit();
        }
    }

    internal class Win11EggyUISetup : Setup
    {
        // Win11 Eggy UI安装
        public static void Start()
        {
            ExpandFile();
            Install_Font();
            Install_StartAllBack();
            Change_StartAllBack_Icon();
            Change_Theme();
            Create_Rainmeter_Task();
            Create_Link();
            Install_FolderBackground();
            MessageBox.Show("Eggy UI 3.0 Win11版安装完成！请重启电脑以应用更改。", "安装完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    internal class Win10EggyUISetup : Setup
    {
        // 'Win10 Eggy UI安装
        public static void Start()
        {
            ExpandFile();
            Install_Font();
            Install_Start11();
            Change_Start11_Icon();
            Change_Theme();
            Create_Rainmeter_Task();
            Create_Link();
            Install_FolderBackground();
            MessageBox.Show("Eggy UI 3.0 Win10版安装完成！请重启电脑以应用更改。", "安装完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
