using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace login2
{
    public partial class PersonInformation : Form
    {
        User user = new User(); // 用户信息
        public PersonInformation(User user)
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

        private void PersonInformation_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        // 窗体加载时
        private void PersonInformation_Load(object sender, EventArgs e)
        {
            tbnickName.Text = user.NickName;
            tbSex.Text = user.Sex;
            tbAge.Text = user.Age.ToString();
            tbMotto.Text = user.Motto;
            tbPhone.Text = user.Phone;
        }

        // 保存按钮
        private void btnSave_Click(object sender, EventArgs e)
        {
            user.NickName = tbnickName.Text;
            user.Sex = tbSex.Text;
            user.Age = int.Parse(tbAge.Text);
            user.Motto = tbMotto.Text;
            user.Phone = tbPhone.Text;

            // 数据库连接
            string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                // UPDATE userslist SET sex = "男", age = 20 WHERE userName = "794735"
                string sql = "UPDATE userslist SET nickName = @newNickName, motto = @newMotto, phone = @newPhone, sex = @newSex, age = @newAge WHERE userName = @name";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@newNickName", user.NickName);
                cmd.Parameters.AddWithValue("@newMotto", user.Motto);
                cmd.Parameters.AddWithValue("@newPhone", user.Phone);
                cmd.Parameters.AddWithValue("@newSex", user.Sex);
                cmd.Parameters.AddWithValue("@newAge", user.Age);
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


            MessageBox.Show("保存成功");
            Main form = new Main(user);
            form.Show();
            this.Close();
        }

        // 关闭按钮
        private void btnClose_Click(object sender, EventArgs e)
        {
            Main form = new Main(user);
            form.Show();
            this.Close();
        }
    }
}
