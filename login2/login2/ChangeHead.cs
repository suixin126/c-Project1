using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login2
{
    public partial class ChangeHead : Form
    {
        User user = new User();
        string head = "";
        public ChangeHead(User user)
        {
            this.user = user;
            InitializeComponent();
        }
        // 拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void ChangeHead_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        // 窗体加载
        private void ChangeHead_Load(object sender, EventArgs e)
        {
            Image image = Image.FromFile(user.Head);
            picHead.BackgroundImage = image;
        }


        // 上传本地图片，修改头像

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult result = open.ShowDialog();
            if (result == DialogResult.OK)
            {
                picHead.BackgroundImage = Image.FromFile(open.FileName);
                head = open.FileName;
                Console.WriteLine(head);
            }
            
        }

        // 保存按钮
        private void button2_Click(object sender, EventArgs e)
        {
            user.Head = head;
            // 同步数据库数据
            string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string sql = "UPDATE userslist SET head = @newHead WHERE userName = @name";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@newHead", user.Head);
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

            Main main = new Main(user);
            main.Show();
            this.Close();
        }

        // 关闭按钮
        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main(user);
            main.Show();
            this.Close();
        }
    }
}
