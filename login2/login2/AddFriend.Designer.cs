namespace login2
{
    partial class AddFriend
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.show = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.picHead = new System.Windows.Forms.PictureBox();
            this.labUserName = new System.Windows.Forms.Label();
            this.labNickName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 14F);
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入账号";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 14F);
            this.textBox1.Location = new System.Drawing.Point(170, 58);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(297, 39);
            this.textBox1.TabIndex = 1;
            // 
            // show
            // 
            this.show.Location = new System.Drawing.Point(473, 57);
            this.show.Name = "show";
            this.show.Size = new System.Drawing.Size(98, 45);
            this.show.TabIndex = 2;
            this.show.Text = "查询";
            this.show.UseVisualStyleBackColor = true;
            this.show.Click += new System.EventHandler(this.show_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(577, 58);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(95, 42);
            this.add.TabIndex = 3;
            this.add.Text = "添加";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // picHead
            // 
            this.picHead.BackColor = System.Drawing.Color.Transparent;
            this.picHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picHead.Location = new System.Drawing.Point(41, 143);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(132, 99);
            this.picHead.TabIndex = 4;
            this.picHead.TabStop = false;
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.BackColor = System.Drawing.Color.Transparent;
            this.labUserName.Font = new System.Drawing.Font("宋体", 20F);
            this.labUserName.Location = new System.Drawing.Point(241, 143);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(0, 40);
            this.labUserName.TabIndex = 5;
            // 
            // labNickName
            // 
            this.labNickName.AutoSize = true;
            this.labNickName.BackColor = System.Drawing.Color.Transparent;
            this.labNickName.Font = new System.Drawing.Font("宋体", 20F);
            this.labNickName.Location = new System.Drawing.Point(241, 202);
            this.labNickName.Name = "labNickName";
            this.labNickName.Size = new System.Drawing.Size(0, 40);
            this.labNickName.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(678, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 39);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AddFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 508);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labNickName);
            this.Controls.Add(this.labUserName);
            this.Controls.Add(this.picHead);
            this.Controls.Add(this.add);
            this.Controls.Add(this.show);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "AddFriend";
            this.Text = "AddFriend";
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button show;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.PictureBox picHead;
        private System.Windows.Forms.Label labUserName;
        private System.Windows.Forms.Label labNickName;
        private System.Windows.Forms.Button btnClose;
    }
}