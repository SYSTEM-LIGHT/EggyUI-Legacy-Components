Imports System.Resources

Public Class Help_Window
    Private BGMEnable As Boolean
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BGMEnable = True '初始化背景音乐状态为开启1
        TreeView1.ExpandAll() '展开帮助内容（节点树）
        PlayPauseBGM() '播放或暂停背景音乐
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PlayPauseBGM() '播放或暂停背景音乐
    End Sub

    Private Sub PlayPauseBGM()
        If (File.Exists("BGM.wav")) Then
            '切换背景音乐播放状态
            If BGMEnable Then
                Button3.Text = "关闭音乐"
                My.Computer.Audio.Play("BGM.wav", AudioPlayMode.BackgroundLoop)
                BGMEnable = False
            Else
                Button3.Text = "开启音乐"
                My.Computer.Audio.Stop()
                BGMEnable = True
            End If
        Else
            Button3.Text = "开启音乐"
            Button3.Enabled = False '禁用按钮
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                      "https://qr16.cn/DlarsA")
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then End '按Esc键关闭窗口
    End Sub
End Class
