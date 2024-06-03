namespace login2
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labNickName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labMotto = new System.Windows.Forms.Label();
            this.labChangeInformation = new System.Windows.Forms.LinkLabel();
            this.labIsOnline = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labNickName1 = new System.Windows.Forms.Label();
            this.picHead = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picMin = new System.Windows.Forms.PictureBox();
            this.picAdd = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // labNickName
            // 
            this.labNickName.AutoSize = true;
            this.labNickName.BackColor = System.Drawing.Color.Transparent;
            this.labNickName.Font = new System.Drawing.Font("宋体", 20F);
            this.labNickName.ForeColor = System.Drawing.Color.White;
            this.labNickName.Location = new System.Drawing.Point(107, 81);
            this.labNickName.Name = "labNickName";
            this.labNickName.Size = new System.Drawing.Size(137, 40);
            this.labNickName.TabIndex = 2;
            this.labNickName.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(12, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 1);
            this.panel1.TabIndex = 3;
            // 
            // labMotto
            // 
            this.labMotto.AutoSize = true;
            this.labMotto.BackColor = System.Drawing.Color.Transparent;
            this.labMotto.Font = new System.Drawing.Font("宋体", 12F);
            this.labMotto.ForeColor = System.Drawing.Color.Gray;
            this.labMotto.Location = new System.Drawing.Point(215, 125);
            this.labMotto.Name = "labMotto";
            this.labMotto.Size = new System.Drawing.Size(58, 24);
            this.labMotto.TabIndex = 2;
            this.labMotto.Text = "格言";
            // 
            // labChangeInformation
            // 
            this.labChangeInformation.AutoSize = true;
            this.labChangeInformation.BackColor = System.Drawing.Color.Transparent;
            this.labChangeInformation.Location = new System.Drawing.Point(280, 87);
            this.labChangeInformation.Name = "labChangeInformation";
            this.labChangeInformation.Size = new System.Drawing.Size(116, 18);
            this.labChangeInformation.TabIndex = 4;
            this.labChangeInformation.TabStop = true;
            this.labChangeInformation.Text = "编辑个人资料";
            this.labChangeInformation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labChangeInformation_LinkClicked);
            // 
            // labIsOnline
            // 
            this.labIsOnline.AutoSize = true;
            this.labIsOnline.BackColor = System.Drawing.Color.Transparent;
            this.labIsOnline.Location = new System.Drawing.Point(113, 131);
            this.labIsOnline.Name = "labIsOnline";
            this.labIsOnline.Size = new System.Drawing.Size(80, 18);
            this.labIsOnline.TabIndex = 2;
            this.labIsOnline.Text = "在线状态";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.panel2.Controls.Add(this.labNickName1);
            this.panel2.Controls.Add(this.picHead);
            this.panel2.Controls.Add(this.labIsOnline);
            this.panel2.Controls.Add(this.labChangeInformation);
            this.panel2.Controls.Add(this.labMotto);
            this.panel2.Controls.Add(this.picClose);
            this.panel2.Controls.Add(this.picMin);
            this.panel2.Location = new System.Drawing.Point(-2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 165);
            this.panel2.TabIndex = 6;
            // 
            // labNickName1
            // 
            this.labNickName1.AutoSize = true;
            this.labNickName1.Font = new System.Drawing.Font("宋体", 14F);
            this.labNickName1.Location = new System.Drawing.Point(113, 81);
            this.labNickName1.Name = "labNickName1";
            this.labNickName1.Size = new System.Drawing.Size(68, 28);
            this.labNickName1.TabIndex = 5;
            this.labNickName1.Text = "昵称";
            // 
            // picHead
            // 
            this.picHead.BackColor = System.Drawing.Color.Transparent;
            this.picHead.BackgroundImage = global::login2.Properties.Resources.动漫3;
            this.picHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHead.Location = new System.Drawing.Point(23, 69);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(80, 80);
            this.picHead.TabIndex = 1;
            this.picHead.TabStop = false;
            this.picHead.Click += new System.EventHandler(this.picHead_Click);
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            this.picClose.BackgroundImage = global::login2.Properties.Resources.close;
            this.picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picClose.Location = new System.Drawing.Point(358, 3);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(49, 45);
            this.picClose.TabIndex = 0;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            this.picClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picClose_MouseDown);
            this.picClose.MouseEnter += new System.EventHandler(this.picClose_MouseEnter);
            this.picClose.MouseLeave += new System.EventHandler(this.picClose_MouseLeave);
            // 
            // picMin
            // 
            this.picMin.BackColor = System.Drawing.Color.Transparent;
            this.picMin.BackgroundImage = global::login2.Properties.Resources.minus;
            this.picMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMin.Location = new System.Drawing.Point(303, 3);
            this.picMin.Name = "picMin";
            this.picMin.Size = new System.Drawing.Size(49, 45);
            this.picMin.TabIndex = 0;
            this.picMin.TabStop = false;
            this.picMin.Click += new System.EventHandler(this.picMin_Click);
            this.picMin.MouseEnter += new System.EventHandler(this.picMin_MouseEnter);
            this.picMin.MouseLeave += new System.EventHandler(this.picMin_MouseLeave);
            // 
            // picAdd
            // 
            this.picAdd.BackColor = System.Drawing.Color.Transparent;
            this.picAdd.BackgroundImage = global::login2.Properties.Resources.添加好友;
            this.picAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picAdd.Location = new System.Drawing.Point(-2, 706);
            this.picAdd.Name = "picAdd";
            this.picAdd.Size = new System.Drawing.Size(73, 61);
            this.picAdd.TabIndex = 7;
            this.picAdd.TabStop = false;
            this.picAdd.Click += new System.EventHandler(this.picAdd_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(406, 768);
            this.Controls.Add(this.picAdd);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labNickName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labNickName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picMin;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label labMotto;
        private System.Windows.Forms.LinkLabel labChangeInformation;
        private System.Windows.Forms.Label labIsOnline;
        private System.Windows.Forms.PictureBox picHead;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labNickName1;
        private System.Windows.Forms.PictureBox picAdd;
    }
}