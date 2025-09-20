namespace EggyUI_Setup
{
    partial class Setup_Window
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup_Window));
            Button1 = new Button();
            LinkLabel1 = new LinkLabel();
            PictureBox1 = new PictureBox();
            linkLabel2 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Button1
            // 
            Button1.Font = new Font("Microsoft YaHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Button1.Location = new Point(310, 450);
            Button1.Name = "Button1";
            Button1.Size = new Size(200, 50);
            Button1.TabIndex = 1;
            Button1.Text = "安装到本机";
            Button1.UseVisualStyleBackColor = true;
            Button1.Click += Button1_Click;
            // 
            // LinkLabel1
            // 
            LinkLabel1.AutoSize = true;
            LinkLabel1.BackColor = Color.Transparent;
            LinkLabel1.Location = new Point(704, 571);
            LinkLabel1.Name = "LinkLabel1";
            LinkLabel1.Size = new Size(84, 20);
            LinkLabel1.TabIndex = 6;
            LinkLabel1.TabStop = true;
            LinkLabel1.Text = "加入交流群";
            LinkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // PictureBox1
            // 
            PictureBox1.BackColor = Color.Transparent;
            PictureBox1.BackgroundImage = Properties.Resources.help;
            PictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            PictureBox1.Location = new Point(487, 346);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(40, 40);
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox1.TabIndex = 4;
            PictureBox1.TabStop = false;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.BackColor = Color.Transparent;
            linkLabel2.Location = new Point(533, 356);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(255, 20);
            linkLabel2.TabIndex = 7;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "我的计算机可以安装Eggy UI 3.0吗？";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // Setup_Window
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background_noicon;
            ClientSize = new Size(800, 600);
            Controls.Add(linkLabel2);
            Controls.Add(LinkLabel1);
            Controls.Add(PictureBox1);
            Controls.Add(Button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Setup_Window";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Eggy UI 3.0安装程序";
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Button Button1;
        internal LinkLabel LinkLabel1;
        internal PictureBox PictureBox1;
        internal LinkLabel linkLabel2;
    }
}
