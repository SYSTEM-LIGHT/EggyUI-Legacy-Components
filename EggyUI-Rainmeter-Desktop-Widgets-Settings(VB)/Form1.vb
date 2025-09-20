Public Class Form1

    ''' <summary>
    ''' EggyUI是由BSOD-MEMZ及其团队开发的Windows美化包，可以在Windows系统上实现类似于游戏《蛋仔派对》的界面风格。
    ''' 这是Eggy UI 3.0 Rainmeter桌面小组件的设置程序，使用VB.NET语言编写（因为之前BSOD-MEMZ用易语言写的设置程序Bug很多，而导致用户体验不好）。
    ''' 这次的这个设置程序加入了更多预设头像，还有修改昵称的功能
    ''' 作者：SYSTEM-LIGHT
    ''' 作者B站主页：https://space.bilibili.com/1591761987
    ''' 作者GitHub主页： https://github.com/SYSTEM-LIGHT
    ''' 蛋仔派对官网：https://party.163.com
    ''' 欢迎来到Eggy UI官方QQ群：882583677
    ''' 有一些代码（包括注释）是用AI写的（当然我也对这些代码修改了一下）
    ''' </summary>

#Region "头像更换功能"

    ''' <summary>
    ''' 这部分代码是用来更换Eggy UI 3.0 Rainmeter桌面小组件头像的功能。
    ''' 头像可以是预设的，也可以是用户自定义的。
    ''' 预设头像存储在当前目录下，文件名为header1.png, header2.png等。
    ''' 用户自定义头像通过文件选择对话框选择，存储在指定的currentheaderfile中。
    ''' </summary>

    Private currentheaderfile As String = "header.png" '指定当前头像文件
    Sub loadheader() '加载头像方法
        If File.Exists(currentheaderfile) Then mainheader.Image = Image.FromFile(currentheaderfile)
    End Sub

    Sub changeheader(changeheaderFile As String) '更换头像方法
        If mainheader.Image IsNot Nothing Then
            mainheader.Image.Dispose() '释放当前头像资源
            mainheader.Image = Nothing '清空当前头像
        End If
        File.Copy(changeheaderFile, currentheaderfile, True) '复制新头像文件到当前头像文件
        loadheader()
    End Sub

    Sub customheaderchange() '更换自定义头像方法
        '更换自定义头像前询问用户
        Dim result As DialogResult = MessageBox.Show(("自定义头像目前为实验性功能。" & vbCrLf & "头像图片要求：分辨率128x128，PNG格式，带Alpha通道，圆形边框（边框的周围一定要全是透明像素）" & vbCrLf & "你确定要更换自定义头像吗？"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            With selectcustomheader
                '设置对话框标题和过滤器
                .Title = "选择自定义头像"
                .Filter = "受支持的图片文件|*.png;"
                .FilterIndex = 1 '默认选择PNG文件
                .RestoreDirectory = True '还原上次选择的目录
                Try
                    If .ShowDialog() = DialogResult.OK Then
                        ' 用户选择了文件
                        Dim selectedFile As String = .FileName
                        ' 复制或处理选中的文件
                        changeheader(selectedFile)
                        MessageBox.Show("自定义头像更换成功，请刷新Rainmeter显示。", "提示",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Exception
                    loadheader()
                    MessageBox.Show("自定义头像更换失败，原因是发生以下异常：" & vbCrLf & ex.Message & vbCrLf & "如果你要反馈此问题，请勿直接反馈给Eggy UI项目组，请将此错误消息截图并发送给@冷落的小情绪。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
            End With
        End If
    End Sub

    Sub presetheaderchange(sender As Object) '更换预设头像方法
        Dim clickedHeader As PictureBox = CType(sender, PictureBox) '获取点击的预设头像控件
        Dim headerfile As String = clickedHeader.Name & ".png" '预设头像文件名
        If File.Exists(headerfile) Then '检查头像文件是否存在
            '替换头像
            changeheader(headerfile)
            MessageBox.Show("头像更换成功，请刷新Rainmeter显示。", "提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            '显示错误消息
            MessageBox.Show("指定的预设头像文件不存在: " & headerfile, "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Header_Click(sender As Object, e As EventArgs) Handles header1.Click,
            header2.Click, header3.Click, header4.Click, header5.Click, header6.Click,
            header7.Click, header8.Click
        '处理预设头像点击事件
        presetheaderchange(sender)
    End Sub

    Private Sub customheader_Click(sender As Object, e As EventArgs) Handles customheader.Click
        '处理自定义头像点击事件
        customheaderchange()
    End Sub

#End Region

#Region "用户昵称更改功能"

    ''' <summary>
    ''' 这部分代码是用来更改Eggy UI 3.0 Rainmeter桌面小组件的当前用户昵称的功能。
    ''' 昵称存储在home.ini文件的第36行（索引为35）中，格式为Text=当前用户昵称。
    ''' loadName方法用于加载当前用户昵称并显示在Label1上。
    ''' changeName方法用于修改当前用户昵称，用户可以通过输入框输入新的用户昵称，点击按钮后更新显示的用户昵称和home.ini文件。
    ''' </summary>

    Sub loadName() '加载昵称方法
        If File.Exists("home.ini") Then
            Dim lines() As String = File.ReadAllLines("home.ini")
            Dim lineIndex As Integer = 35 '第36行，索引从0开始
            Dim Name As String = ""
            If lines.Length > lineIndex Then
                Dim line As String = lines(lineIndex)
                If line.StartsWith("Text=") Then
                    Name = line.Substring("Text=".Length).Trim()
                Else
                    Name = line.Trim()
                End If
            End If
            Label1.Text = Name '设置Label1的文本为读取到的用户昵称
        End If
    End Sub

    Sub changeName() '修改昵称方法
        Dim newName As String = InputBox("请输入新的昵称：", "更改昵称")
        If Not String.IsNullOrWhiteSpace(newName) Then
            '如果用户输入了新的用户昵称，则更新home.ini文件和显示的用户昵称
            Dim lines() As String = File.ReadAllLines("home.ini") '读取home.ini文件的所有行
            Dim lineIndex As Integer = 35 '第36行，索引从0开始
            If lines.Length > lineIndex Then
                lines(lineIndex) = "Text=" & newName
                File.WriteAllLines("home.ini", lines, System.Text.Encoding.Unicode)
            End If
            Label1.Text = newName
            MessageBox.Show("昵称更改成功，请刷新Rainmeter显示。", "提示", MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub changeNameButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        changeName()
    End Sub

#End Region

#Region "重置Rainmeter组件"

    Sub ResetRainmeter()
        Dim fsw As New StreamWriter(homefile, False, System.Text.Encoding.Unicode)
        fsw.Write("[Rainmeter]" + vbCrLf +
                  "Update=1000" + vbCrLf +
                  "Logging=0" + vbCrLf +
                  "SkinPath=%USERPROFILE%\Documents\Rainmeter\Skins\" +
                  vbCrLf + vbCrLf +
                  "[MeterLaunch1Image]" + vbCrLf +
                  "Meter=Image" + vbCrLf +
                  "ImageName=home.png" + vbCrLf +
                  "W=285" + vbCrLf +
                  "H=96" + vbCrLf +
                  ";在W,H中设置图像的长和宽" + vbCrLf +
                  "X=32" + vbCrLf +
                  "Y=32" + vbCrLf +
                  "LeftMouseUpAction=[""%USERPROFILE%\Documents\Rainmeter\Skins\EggyUI\Home\EggyUIDesktopWidgetsSettings.exe""]" +
                  vbCrLf + vbCrLf +
                  "[MeterLaunch1tx]" + vbCrLf +
                  "Meter=Image" + vbCrLf +
                  "ImageName=header.png" + vbCrLf +
                  "W=75" + vbCrLf +
                  "H=75" + vbCrLf +
                  ";在W,H中设置图像的长和宽" + vbCrLf +
                  "X=43" + vbCrLf +
                  "Y=43" + vbCrLf +
                  "LeftMouseUpAction=[""%USERPROFILE%\Documents\Rainmeter\Skins\EggyUI\Home\EggyUIDesktopWidgetsSettings.exe""]" +
                  vbCrLf + vbCrLf +
                  "[MeterLaunch1Text]" + vbCrLf +
                  "Meter=String" + vbCrLf +
                  "X=133" + vbCrLf +
                  "Y=50" + vbCrLf +
                  "FontFace=Impact" + vbCrLf +
                  "FontSize=14" + vbCrLf +
                  "FontColor=255,255,255,255" + vbCrLf +
                  ";StringStyle=Bold" + vbCrLf +
                  "SolidColor=0,0,0,1" + vbCrLf +
                  "AntiAlias=1" + vbCrLf +
                  "Text=Eggy" + vbCrLf +
                  ";将Text=后面的内容修改为您的用户名" + vbCrLf +
                  "LeftMouseUpAction=[""%USERPROFILE%\Documents\Rainmeter\Skins\EggyUI\Home\EggyUIDesktopWidgetsSettings.exe""]" + vbCrLf + vbCrLf)
        fsw.Close()
        If mainheader.Image IsNot Nothing Then
            mainheader.Image.Dispose()
            mainheader.Image = Nothing
        End If
        changeheader("header1.png")
        loadName()
        MessageBox.Show("重置此Rainmeter小组件成功！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ResetRainmeter()
    End Sub

#End Region


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '处理窗口加载事件
        loadheader() '加载当前头像
        loadName() '加载当前用户昵称
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then End '按下ESC键退出程序
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        MessageBox.Show("此功能暂未开放。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
