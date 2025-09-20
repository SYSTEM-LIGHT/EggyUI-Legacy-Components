<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Help_Window
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TreeNode1 As TreeNode = New TreeNode("答：进入文档\Rainmeter\Skins\EggyUI\Home，修改文件夹内的home.ini文件，不用担心找不到在哪里改，home.ini里有对应的注释。")
        Dim TreeNode2 As TreeNode = New TreeNode("如何修改Rainmeter桌面小组件的用户名？", New TreeNode() {TreeNode1})
        Dim TreeNode3 As TreeNode = New TreeNode("答：和修改用户名一样，找到对应组件的ini，修改" & ChrW(8220) & "W" & ChrW(8221) & "和" & ChrW(8220) & "H" & ChrW(8221) & "（分别对应长和宽，一定要等比例缩放哦！）")
        Dim TreeNode4 As TreeNode = New TreeNode("如何修改Rainmeter桌面小工具尺寸？", New TreeNode() {TreeNode3})
        Dim TreeNode5 As TreeNode = New TreeNode("答：进入Rainmeter管理器，然后点击" & ChrW(8220) & "刷新全部（或Refresh All）" & ChrW(8221) & "，重启Rainmeter也行。")
        Dim TreeNode6 As TreeNode = New TreeNode("我修改了Rainmeter桌面小组件的皮肤，但修改似乎并未生效？", New TreeNode() {TreeNode5})
        Dim TreeNode7 As TreeNode = New TreeNode("答：这是因为你没安装显卡驱动，安装显卡驱动就可以解决这个问题了。")
        Dim TreeNode8 As TreeNode = New TreeNode("任务栏没有透明效果，窗口呈直角，系统明显卡顿？", New TreeNode() {TreeNode7})
        Dim TreeNode9 As TreeNode = New TreeNode("答：进入C:\EggyResource\FolderBackground\image，修改图像文件（图片分辨率建议不要太大，而且颜色不要太花，不然瞎眼，只支持JPG和PNG格式的图片，如果出了问题按住ESC再进入文件资源管理器）")
        Dim TreeNode10 As TreeNode = New TreeNode("如何修改文件夹背景？", New TreeNode() {TreeNode9})
        Dim TreeNode11 As TreeNode = New TreeNode("by EggyUI项目组")
        Dim TreeNode12 As TreeNode = New TreeNode("程序编写：@BSOD-MEMZ & @冷落的小情绪")
        Dim TreeNode13 As TreeNode = New TreeNode("欢迎加入EggyUI交流群：882583677")
        Dim TreeNode14 As TreeNode = New TreeNode("作者信息", New TreeNode() {TreeNode11, TreeNode12, TreeNode13})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Help_Window))
        Label1 = New Label()
        TreeView1 = New TreeView()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("方正兰亭圆_GBK_中", 13.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Label1.Location = New Point(60, 48)
        Label1.Name = "Label1"
        Label1.Size = New Size(395, 27)
        Label1.TabIndex = 0
        Label1.Text = "已列出部分常见问题，希望能帮助您~"
        ' 
        ' TreeView1
        ' 
        TreeView1.BackColor = SystemColors.Window
        TreeView1.Font = New Font("方正兰亭圆_GBK_中", 9.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        TreeView1.Location = New Point(60, 78)
        TreeView1.Margin = New Padding(3, 4, 3, 4)
        TreeView1.Name = "TreeView1"
        TreeNode1.Name = "节点1"
        TreeNode1.Text = "答：进入文档\Rainmeter\Skins\EggyUI\Home，修改文件夹内的home.ini文件，不用担心找不到在哪里改，home.ini里有对应的注释。"
        TreeNode2.Name = "节点0"
        TreeNode2.Text = "如何修改Rainmeter桌面小组件的用户名？"
        TreeNode3.Name = "节点4"
        TreeNode3.Text = "答：和修改用户名一样，找到对应组件的ini，修改" & ChrW(8220) & "W" & ChrW(8221) & "和" & ChrW(8220) & "H" & ChrW(8221) & "（分别对应长和宽，一定要等比例缩放哦！）"
        TreeNode4.Name = "节点2"
        TreeNode4.Text = "如何修改Rainmeter桌面小工具尺寸？"
        TreeNode5.Name = "节点5"
        TreeNode5.Text = "答：进入Rainmeter管理器，然后点击" & ChrW(8220) & "刷新全部（或Refresh All）" & ChrW(8221) & "，重启Rainmeter也行。"
        TreeNode6.Name = "节点3"
        TreeNode6.Text = "我修改了Rainmeter桌面小组件的皮肤，但修改似乎并未生效？"
        TreeNode7.Name = "节点9"
        TreeNode7.Text = "答：这是因为你没安装显卡驱动，安装显卡驱动就可以解决这个问题了。"
        TreeNode8.Name = "节点7"
        TreeNode8.Text = "任务栏没有透明效果，窗口呈直角，系统明显卡顿？"
        TreeNode9.Name = "节点12"
        TreeNode9.Text = "答：进入C:\EggyResource\FolderBackground\image，修改图像文件（图片分辨率建议不要太大，而且颜色不要太花，不然瞎眼，只支持JPG和PNG格式的图片，如果出了问题按住ESC再进入文件资源管理器）"
        TreeNode10.Name = "节点11"
        TreeNode10.Text = "如何修改文件夹背景？"
        TreeNode11.Name = "节点14"
        TreeNode11.Text = "by EggyUI项目组"
        TreeNode12.Name = "节点15"
        TreeNode12.Text = "程序编写：@BSOD-MEMZ & @冷落的小情绪"
        TreeNode13.Name = "节点16"
        TreeNode13.Text = "欢迎加入EggyUI交流群：882583677"
        TreeNode14.Name = "节点13"
        TreeNode14.Text = "作者信息"
        TreeView1.Nodes.AddRange(New TreeNode() {TreeNode2, TreeNode4, TreeNode6, TreeNode8, TreeNode10, TreeNode14})
        TreeView1.Size = New Size(644, 276)
        TreeView1.TabIndex = 1
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("方正兰亭圆_GBK_中", 9.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Button1.Location = New Point(600, 362)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(104, 29)
        Button1.TabIndex = 2
        Button1.Text = "关闭本窗口"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Font = New Font("方正兰亭圆_GBK_中", 9.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Button2.Location = New Point(500, 362)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 29)
        Button2.TabIndex = 3
        Button2.Text = "联机帮助"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Font = New Font("方正兰亭圆_GBK_中", 9.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Button3.Location = New Point(400, 362)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(94, 29)
        Button3.TabIndex = 4
        Button3.Text = "关闭音乐"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Help_Window
        ' 
        AutoScaleDimensions = New SizeF(9.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), Image)
        BackgroundImageLayout = ImageLayout.Zoom
        ClientSize = New Size(765, 425)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(TreeView1)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        KeyPreview = True
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        Name = "Help_Window"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "手册"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button

End Class
