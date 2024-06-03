namespace login2
{
    partial class Chat
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
            this.components = new System.ComponentModel.Container();
            this.labNickName = new System.Windows.Forms.Label();
            this.rtbReceive = new System.Windows.Forms.RichTextBox();
            this.rtbSend = new System.Windows.Forms.RichTextBox();
            this.btnSendText = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSendEmotion = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picMax = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picMin = new System.Windows.Forms.PictureBox();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.picMore = new System.Windows.Forms.PictureBox();
            this.picEmotion = new System.Windows.Forms.PictureBox();
            this.picFile = new System.Windows.Forms.PictureBox();
            this.picCut = new System.Windows.Forms.PictureBox();
            this.picImg = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmotion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImg)).BeginInit();
            this.SuspendLayout();
            // 
            // labNickName
            // 
            this.labNickName.AutoSize = true;
            this.labNickName.BackColor = System.Drawing.Color.Transparent;
            this.labNickName.Font = new System.Drawing.Font("宋体", 20F);
            this.labNickName.Location = new System.Drawing.Point(215, 9);
            this.labNickName.Name = "labNickName";
            this.labNickName.Size = new System.Drawing.Size(137, 40);
            this.labNickName.TabIndex = 0;
            this.labNickName.Text = "label1";
            // 
            // rtbReceive
            // 
            this.rtbReceive.Location = new System.Drawing.Point(2, 62);
            this.rtbReceive.Name = "rtbReceive";
            this.rtbReceive.Size = new System.Drawing.Size(741, 359);
            this.rtbReceive.TabIndex = 1;
            this.rtbReceive.Text = "";
            // 
            // rtbSend
            // 
            this.rtbSend.BackColor = System.Drawing.Color.White;
            this.rtbSend.Location = new System.Drawing.Point(2, 476);
            this.rtbSend.Name = "rtbSend";
            this.rtbSend.Size = new System.Drawing.Size(710, 84);
            this.rtbSend.TabIndex = 2;
            this.rtbSend.Text = "";
            // 
            // btnSendText
            // 
            this.btnSendText.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSendText.Location = new System.Drawing.Point(101, 575);
            this.btnSendText.Name = "btnSendText";
            this.btnSendText.Size = new System.Drawing.Size(106, 48);
            this.btnSendText.TabIndex = 3;
            this.btnSendText.Text = "发送文字";
            this.btnSendText.UseVisualStyleBackColor = false;
            this.btnSendText.Click += new System.EventHandler(this.btnSendText_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSendEmotion
            // 
            this.btnSendEmotion.BackColor = System.Drawing.Color.Aquamarine;
            this.btnSendEmotion.Location = new System.Drawing.Point(314, 575);
            this.btnSendEmotion.Name = "btnSendEmotion";
            this.btnSendEmotion.Size = new System.Drawing.Size(109, 48);
            this.btnSendEmotion.TabIndex = 6;
            this.btnSendEmotion.Text = "发送表情";
            this.btnSendEmotion.UseVisualStyleBackColor = false;
            this.btnSendEmotion.Click += new System.EventHandler(this.btnSendEmotion_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.OldLace;
            this.panel1.Controls.Add(this.picClose);
            this.panel1.Controls.Add(this.picMax);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.picMin);
            this.panel1.Controls.Add(this.labNickName);
            this.panel1.Controls.Add(this.pic1);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 56);
            this.panel1.TabIndex = 10;
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            this.picClose.BackgroundImage = global::login2.Properties.Resources.关闭;
            this.picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picClose.Location = new System.Drawing.Point(698, 12);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(35, 35);
            this.picClose.TabIndex = 11;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picMax
            // 
            this.picMax.BackColor = System.Drawing.Color.Transparent;
            this.picMax.BackgroundImage = global::login2.Properties.Resources.Maximize_1;
            this.picMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMax.Location = new System.Drawing.Point(644, 12);
            this.picMax.Name = "picMax";
            this.picMax.Size = new System.Drawing.Size(35, 35);
            this.picMax.TabIndex = 11;
            this.picMax.TabStop = false;
            this.picMax.Click += new System.EventHandler(this.picMax_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::login2.Properties.Resources.空间;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(340, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // picMin
            // 
            this.picMin.BackColor = System.Drawing.Color.Transparent;
            this.picMin.BackgroundImage = global::login2.Properties.Resources.minus;
            this.picMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMin.Location = new System.Drawing.Point(590, 12);
            this.picMin.Name = "picMin";
            this.picMin.Size = new System.Drawing.Size(35, 35);
            this.picMin.TabIndex = 11;
            this.picMin.TabStop = false;
            this.picMin.Click += new System.EventHandler(this.picMin_Click);
            // 
            // pic1
            // 
            this.pic1.BackColor = System.Drawing.Color.Transparent;
            this.pic1.BackgroundImage = global::login2.Properties.Resources.xiala;
            this.pic1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pic1.Location = new System.Drawing.Point(535, 12);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(35, 35);
            this.pic1.TabIndex = 11;
            this.pic1.TabStop = false;
            // 
            // picMore
            // 
            this.picMore.BackColor = System.Drawing.Color.Transparent;
            this.picMore.BackgroundImage = global::login2.Properties.Resources.shenglvehao;
            this.picMore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMore.Location = new System.Drawing.Point(264, 430);
            this.picMore.Name = "picMore";
            this.picMore.Size = new System.Drawing.Size(40, 40);
            this.picMore.TabIndex = 9;
            this.picMore.TabStop = false;
            this.picMore.Click += new System.EventHandler(this.picMore_Click);
            // 
            // picEmotion
            // 
            this.picEmotion.BackColor = System.Drawing.Color.Transparent;
            this.picEmotion.BackgroundImage = global::login2.Properties.Resources.biaoqingfuhao;
            this.picEmotion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEmotion.Location = new System.Drawing.Point(196, 430);
            this.picEmotion.Name = "picEmotion";
            this.picEmotion.Size = new System.Drawing.Size(40, 40);
            this.picEmotion.TabIndex = 9;
            this.picEmotion.TabStop = false;
            this.picEmotion.Click += new System.EventHandler(this.picEmotion_Click);
            // 
            // picFile
            // 
            this.picFile.BackColor = System.Drawing.Color.Transparent;
            this.picFile.BackgroundImage = global::login2.Properties.Resources.wenjian;
            this.picFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFile.Location = new System.Drawing.Point(131, 430);
            this.picFile.Name = "picFile";
            this.picFile.Size = new System.Drawing.Size(40, 40);
            this.picFile.TabIndex = 9;
            this.picFile.TabStop = false;
            this.picFile.Click += new System.EventHandler(this.picFile_Click);
            // 
            // picCut
            // 
            this.picCut.BackColor = System.Drawing.Color.Transparent;
            this.picCut.BackgroundImage = global::login2.Properties.Resources.jieping;
            this.picCut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picCut.Location = new System.Drawing.Point(67, 430);
            this.picCut.Name = "picCut";
            this.picCut.Size = new System.Drawing.Size(40, 40);
            this.picCut.TabIndex = 9;
            this.picCut.TabStop = false;
            this.picCut.Click += new System.EventHandler(this.picCut_Click);
            // 
            // picImg
            // 
            this.picImg.BackColor = System.Drawing.Color.Transparent;
            this.picImg.BackgroundImage = global::login2.Properties.Resources.tupian;
            this.picImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picImg.Location = new System.Drawing.Point(12, 430);
            this.picImg.Name = "picImg";
            this.picImg.Size = new System.Drawing.Size(40, 40);
            this.picImg.TabIndex = 9;
            this.picImg.TabStop = false;
            this.picImg.Click += new System.EventHandler(this.picImg_Click);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(748, 635);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picMore);
            this.Controls.Add(this.picEmotion);
            this.Controls.Add(this.picFile);
            this.Controls.Add(this.picCut);
            this.Controls.Add(this.picImg);
            this.Controls.Add(this.btnSendEmotion);
            this.Controls.Add(this.btnSendText);
            this.Controls.Add(this.rtbSend);
            this.Controls.Add(this.rtbReceive);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Chat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Chat_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Chat_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Chat_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmotion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labNickName;
        private System.Windows.Forms.RichTextBox rtbReceive;
        private System.Windows.Forms.Button btnSendText;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.RichTextBox rtbSend;
        private System.Windows.Forms.PictureBox picImg;
        private System.Windows.Forms.PictureBox picCut;
        private System.Windows.Forms.PictureBox picFile;
        private System.Windows.Forms.PictureBox picEmotion;
        private System.Windows.Forms.PictureBox picMore;
        private System.Windows.Forms.Button btnSendEmotion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.PictureBox picMin;
        private System.Windows.Forms.PictureBox picMax;
        private System.Windows.Forms.PictureBox picClose;
    }
}