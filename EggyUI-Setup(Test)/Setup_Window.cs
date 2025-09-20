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
                MessageBox.Show("��ǰϵͳ��֧�ְ�װEggy UI 3.0���뽫ϵͳ������Win10��Win11��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("��ӭ����Eggy UI�ٷ�QQȺ��882583677", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((buildNumber >= 10240) && (buildNumber < 21994))
            {
                MessageBox.Show("��ĵ���֧�ְ�װEggy UI 3.0 Win10�档", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (buildNumber >= 21994)
            {
                MessageBox.Show("��ĵ���֧�ְ�װEggy UI 3.0 Win11�档", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("��ǰϵͳ��֧�ְ�װEggy UI 3.0���뽫ϵͳ������Win10 2004��Win11 22H2���ϰ汾��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
