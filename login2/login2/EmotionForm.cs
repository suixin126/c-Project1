using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login2
{
    public partial class EmotionForm : Form
    {

        Chat form1;

        public EmotionForm(Chat form)
        {
           // m_aeroEnabled = false;
            InitializeComponent();
            form1 = form;
        }

        // 窗体加载
        private void EmotionForm_Load(object sender, EventArgs e)
        {
            //显示表情图像 4*5的框
            for (int t = 0; t < 4; t++)
            {
                for (int i = 0; i < 5; i++)
                {
                    PictureBox Ps = new PictureBox();
                    Ps.Size = new Size(50, 50);
                    Ps.SizeMode = PictureBoxSizeMode.Zoom; // 适应比例缩放
                    Ps.Image = Image.FromFile(@"Emotion\" + ((i + 1) + (t * 5)) + ".png"); // 获取图片信息
                    Ps.Location = new Point(i * 30 + 60 * (i + 1), 15 + (t * 60)); // 图片显示位置
                    Ps.Cursor = Cursors.Hand; // 光标设置为手
                    Ps.BackColor = Color.Transparent; // 背景颜色设置为透明
                    Ps.Tag = ((i + 1) + (t * 5)) + ".png"; // 图片唯一标识下标
                    Ps.Click += new EventHandler(SmailPic_Click); // 增添点击事件
                    this.Controls.Add(Ps);
                }
            }
        }

        // 点击获取对应表情
        private void SmailPic_Click(object sender, EventArgs e)
        {
            // 关联到聊天框，向输入框写入图片
            PictureBox psm = (PictureBox)sender;
            form1.rtbSend.AppendText("C:\\houduanstudy\\c#\\login2\\login2\\bin\\Debug\\Emotion\\" + psm.Tag);

            Bitmap bmp = new Bitmap(@"Emotion\" + psm.Tag);//获得图片

            Bitmap bmpSmall = new Bitmap(bmp, 22, 22); // 输入框中的图片大小
            Clipboard.SetDataObject(bmpSmall, false);//将图片放在剪贴板中

            if (form1.rtbSend.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))

                form1.rtbSend.Paste();//粘贴数据

            // 关闭窗体
            Close();
        }
    }
}
