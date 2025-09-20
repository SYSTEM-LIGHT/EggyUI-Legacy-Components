namespace EggyUI_Setup
{
    public partial class Setup_Window : Form
    {
        private static readonly int buildNumber = Environment.OSVersion.Version.Build;

        public Setup_Window()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if ((buildNumber >= 10240) && (buildNumber < 21994))
            {
                this.Close();
                Win10EggyUISetup.Start();
            }
            else if (buildNumber >= 21994)
            {
                this.Close();
                Win11EggyUISetup.Start();
            }
            else
            {
                MessageBox.Show("当前系统不支持安装Eggy UI 3.0，请将系统升级到Win10或Win11。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("欢迎加入Eggy UI官方QQ群：882583677", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((buildNumber >= 10240) && (buildNumber < 21994))
            {
                MessageBox.Show("你的电脑支持安装Eggy UI 3.0 Win10版。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (buildNumber >= 21994)
            {
                MessageBox.Show("你的电脑支持安装Eggy UI 3.0 Win11版。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("当前系统不支持安装Eggy UI 3.0，请将系统升级到Win10 2004或Win11 22H2以上版本。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
