using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login2
{
    public partial class Main : Form
    {
        User user = new User(); // 用户数据
        public Main(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        // 获取圆形图片
        private Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }

        // 拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        // 窗体加载
        private void Main_Load(object sender, EventArgs e)
        {
            Console.WriteLine(user.IsOnline);

            // 设置在线状态
            this.labIsOnline.Text = "在线";
            this.labIsOnline.ForeColor = Color.Green;

            // 设置头像
            //user.Head = user.Head.Replace("\"", "");
            Image image = Image.FromFile(user.Head);
            this.picHead.BackgroundImage = CutEllipse(image, new Rectangle(0, 0, image.Width, image.Height), new Size(80, 80));

            // 设置昵称
            this.labNickName1.Text = user.NickName;

            // 设置座右铭，长度过长的话省略
            if (user.Motto.Length > 10)
            {
                user.Motto = user.Motto.Substring(0, 10) + "...";
            }
            this.labMotto.Text = user.Motto;

            Console.WriteLine(user.Friends.Count);

            // 显示好友列表
            int hIndex = 0;
            int nIndex = 0;
            int oIndex = 0;
            int index = 0; // 下划线
            foreach (var item in user.Friends)
            {
               // Console.WriteLine("好友昵称：" + item.NickName + "好友头像：" + item.Head + "好友在线状态：" + item.IsOnline);

                item.Head = item.Head.Replace("\"", "");
                Image headImage = Image.FromFile(item.Head);

                // 头像
                PictureBox pictureBox = new PictureBox();
                int x = 15;
                int y = 130 + hIndex * 70;
                pictureBox.Location = new Point(x, y);
                pictureBox.Size = new Size(50, 50);
                pictureBox.BackgroundImage = CutEllipse(headImage, new Rectangle(0, 0, headImage.Width, headImage.Height), new Size(50, 50));
                pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox.BackColor = Color.Transparent;
                hIndex++;

                // 昵称
                Label labelNickName = new Label();
                int nameX = 80;
                int nameY = 130 + nIndex * 70;
                labelNickName.Location = new Point(nameX, nameY);
                labelNickName.Font = new Font("宋体", 15);
                labelNickName.BackColor = Color.Transparent;
                labelNickName.ForeColor = Color.Pink;
                labelNickName.Text = item.NickName;

                // 下划线
                Panel panel = new Panel();
                int px = 0;
                int py = 195 + index * 70;
                panel.Location = new Point(px, py);
                panel.Size = new Size(406, 1);
                panel.BackColor = Color.Gray;
                index++;

                // 绑定双击事件
                labelNickName.DoubleClick += LabelNickName_DoubleClick;

                nIndex++;

                // 在线状态
                Label labelIsOnline = new Label();
                int isOnlineX = 70;
                int isOnlineY = 170 + oIndex * 70;
                labelIsOnline.Location = new Point(isOnlineX, isOnlineY);
                labelIsOnline.Font = new Font("宋体", 9);
                labelIsOnline.BackColor = Color.Transparent;
                if (item.IsOnline == 1)
                {
                    labelIsOnline.Text = "在线";
                    labelIsOnline.ForeColor = Color.Green;
                }
                else if (item.IsOnline == 0)
                {
                    labelIsOnline.Text = "未在线";
                    labelIsOnline.ForeColor = Color.Gray;
                }
                oIndex++;

                // 添加控件到窗体
                this.Controls.Add(pictureBox);
                this.Controls.Add(labelNickName);
                this.Controls.Add(labelIsOnline);
                this.Controls.Add(panel);
            }
        }

        // 双击打开聊天窗体
        private void LabelNickName_DoubleClick(object sender, EventArgs e)
        {
            // 获取好友昵称
            Label label = (Label)sender;
            string nickName = label.Text;
            Chat chat = new Chat(user.NickName,nickName);
            chat.Show();
        }


        // 最小化
        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void picMin_MouseEnter(object sender, EventArgs e)
        {
            picMin.BackColor = Color.Gray;
        }

        private void picMin_MouseLeave(object sender, EventArgs e)
        {
            picMin.BackColor = Color.Transparent;
        }

        // 关闭窗体
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();

            // 退出主界面的时候 关闭程序并设置用户在线状态为0
            string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string sql = "UPDATE userslist SET isOnline = @newIsOnline WHERE userName = @name";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@newIsOnline", 0);
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            Process.GetCurrentProcess().Kill();
        }

        private void picClose_MouseDown(object sender, MouseEventArgs e)
        {
            picClose.BackColor = Color.Red;
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            picClose.BackColor = Color.Transparent;
        }

        private void picClose_MouseEnter(object sender, EventArgs e)
        {
            picClose.BackColor = Color.Red;
        }


        // 编辑个人资料信息
        private void labChangeInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonInformation personInformation = new PersonInformation(user);
            personInformation.Show();
            this.Close();
        }

        // 更换头像
        private void picHead_Click(object sender, EventArgs e)
        {
            ChangeHead changeHead = new ChangeHead(user);
            changeHead.Show();
            this.Close();
        }

        // 添加好友
        private void picAdd_Click(object sender, EventArgs e)
        {
            AddFriend addFriend = new AddFriend(user);
            addFriend.Show();
            this.Close();
        }
    }
}
