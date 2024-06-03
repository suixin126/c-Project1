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
using System.Web.Security;
using System.Windows.Forms;


namespace login2
{
   public partial class Login : Form
    {
        


        public UserList userList = new UserList();
        // 判断是否登录成功
        bool isSuccess = false;
        public Login()
        {
            InitializeComponent();

            textBox1.LostFocus += new EventHandler(user_LostFocus);
            textBox1.GotFocus += new EventHandler(user_GotFocus);
            textBox2.LostFocus += new EventHandler(pwd_LostFocus);
            textBox2.GotFocus += new EventHandler(pwd_GotFocus);
            txtValidCode.LostFocus += new EventHandler(Valid_LostFocus);
            txtValidCode.GotFocus += new EventHandler(Valid_GotFocus);

            //pictureBox8.Click += new EventHandler(pictureBox8_Click);

            button1.Click += new EventHandler(button1_Click);
        }

        // 拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        //添加窗体的MouseDown事件，并编写如下代码
        private void Login_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void tishi()
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "请输入账号";
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "请输入密码";
                textBox2.PasswordChar = '\0'; //清空PasswordChar设置
            }

            if (txtValidCode.Text == "")
            {
                txtValidCode.Text = "请输入验证码";
            }
        }
        // 账号获取焦点
        private void user_GotFocus(object sender, EventArgs e)
        {
            panel1.BackColor = Color.SkyBlue;
            panel2.BackColor = Color.Gray;
            panel3.BackColor = Color.Gray;
            if (textBox1.Text == "请输入账号")
            {
                textBox1.Text = "";
            }
        }

        private void user_LostFocus(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Gray;
            if (textBox1.Text == "")
            {
                textBox1.Text = "请输入账号";
            }
        }

        // 密码获取焦点

        private void pwd_GotFocus(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.SkyBlue;
            panel3.BackColor = Color.Gray;
            textBox2.PasswordChar = char.Parse("*"); //设置密码框显示字符为*
            if (textBox2.Text == "请输入密码")
            {
                textBox2.Text = "";
            }
        }

        private void pwd_LostFocus(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Gray;
            if (textBox2.Text == "")
            {
                textBox2.Text = "请输入密码";
                textBox2.PasswordChar = '\0';
            }
        }
        //验证码获取焦点
        private void Valid_GotFocus(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.Gray;
            panel3.BackColor = Color.SkyBlue;
            if (txtValidCode.Text == "请输入验证码")
            {
                txtValidCode.Text = "";
            }
        }

        private void Valid_LostFocus(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Gray;
            if (txtValidCode.Text == "")
            {
                txtValidCode.Text = "请输入验证码";
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        #region 验证码
        private const int ValidCodeLength = 4;//验证码长度        
        private String strValidCode = "";//验证码                        

        //调用自定义函数,更新验证码
        private void UpdateValidCode()
        {
            strValidCode = ValidCode.CreateRandomCode(ValidCodeLength);//生成随机验证码
            if (strValidCode == "") return;
            ValidCode.CreateImage(strValidCode, pictureBox1);//创建验证码图片
        }
        #endregion


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateValidCode();//点击更新验证码
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateValidCode();//加载更新验证码
        }

        ///  验证码（不区分大小写） 
        //点击登录按钮
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            int goalIndex = 0; // 获取目标用户索引
            Console.WriteLine(isSuccess);

            //获取用户输入的账户名和密码
            string account = string.Format(textBox1.Text);
            string password = string.Format(textBox2.Text);
            // 密码加密
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");

            //string yanzheng = string.Format();


            //获取用户验证码
            string validcode = txtValidCode.Text.Trim();
            if (String.IsNullOrEmpty(validcode) != true)//验证码不为空
            {
                if (validcode.ToLower() == strValidCode.ToLower())
                {
                    MessageBox.Show("验证通过", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValidCode.Text = "";
                    txtValidCode.Focus();
                }
                else
                {
                    MessageBox.Show("验证失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtValidCode.Text = "";
                    txtValidCode.Focus();
                }
            }
            else//验证码为空
            {
                MessageBox.Show("请输入验证码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtValidCode.Text = "";
                txtValidCode.Focus();
            }

            // 跳转主页面
            foreach (var item in userList.ListUsers)
            {
                if (item.UserName.Equals(account) && item.Password.Equals(pwd) && validcode.ToLower().Equals(strValidCode.ToLower()))
                {
                    isSuccess = true;
                    item.IsOnline = 1;
                    goalIndex = userList.ListUsers.IndexOf(item);

                    // 同步数据库数据
                    string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    try
                    {
                        connection.Open();
                        string sql = "UPDATE userslist SET isOnline = @newIsOnline WHERE userName = @name";
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@newIsOnline", 1);
                        cmd.Parameters.AddWithValue("@name", item.UserName);
                        cmd.ExecuteNonQuery();
                    }catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                    break;
                }
                else
                {
                    isSuccess = false;
                }
            }
            if (isSuccess == true)
            {
                UpdateValidCode();//更新验证码
                MessageBox.Show("登录成功");
                Main form = new Main(userList.ListUsers[goalIndex]);
                form.Show();
                this.Close();
            }
            else
            {
                UpdateValidCode();//更新验证码
                MessageBox.Show("登陆失败");
            }
        }

        //点击注册链接，跳转注册页面
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register form2 = new Register();
            this.Hide();
            form2.ShowDialog();
            this.Dispose();
        }

        //验证码图标
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        //关闭设置
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Process.GetCurrentProcess().Kill();
        }

        //最小化设置
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //点击隐藏密码图标
        Boolean showPW = true;
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            //Boolean showPW = false;
            //showPW = !showPW;
            if (showPW)
            {
                textBox2.PasswordChar = '\0';
                pictureBox8.BackgroundImage = Properties.Resources.眼睛_显示_o;
                showPW = false;
            }
            else
            {
                textBox2.PasswordChar = '*';
                pictureBox8.BackgroundImage = Properties.Resources.眼睛_隐藏_o;
                showPW = true;
            }

        }


        // 窗体加载的时候
        private void Form1_Load_1(object sender, EventArgs e)
        {
            UpdateValidCode();//加载更新验证码
            tishi(); //
            string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string sql = "SELECT * FROM userslist"; // 查询语句
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                // 将数据库内容添加到用户列表
                while (reader.Read())
                {
                    // 0：用户名 1：密码 2：昵称 3：格言 4：头像 5：电话 6：是否在线
                    User user = new User();
                    user.UserName = reader[0].ToString();
                    user.Password = reader[1].ToString();
                    user.NickName = reader[2].ToString();
                    user.Motto = reader[3].ToString();
                    user.Head = reader[4].ToString();
                    user.Phone = reader[5].ToString();
                    user.IsOnline = int.Parse(reader[6].ToString());
                    userList.ListUsers.Add(user);
                }
                foreach (var item in userList.ListUsers)
                {
                    Console.WriteLine("用户名：" + item.UserName + ",密码：" + item.Password + ",昵称：" + item.NickName + ",座右铭：" + item.Motto + ",头像：" + item.Head + "，电话：" + item.Phone + ",是否上线：" + item.IsOnline);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // 获取好友列表
            try
            {
                connection.Open();

                string sql2 = "SELECT * FROM friends";
                MySqlCommand cmd2 = new MySqlCommand(sql2, connection);
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    int index = 1;
                    while (dr2[index].ToString() != "")
                    {
                        Friends friends = new Friends();
                        friends.OwnUserName = dr2[0].ToString();
                        friends.FriendName = dr2[index].ToString();
                        // 用户列表的好友信息
                        foreach (var item in userList.ListUsers)
                        {
                            if (item.UserName.Equals(friends.FriendName))
                            {
                                friends.FriendName = item.UserName;
                                friends.NickName = item.NickName;
                                friends.Head = item.Head;
                                friends.IsOnline = item.IsOnline;
                                break;
                            }
                        }
                        for (int i = 0; i < userList.ListUsers.Count; i++)
                        {
                            if (friends.OwnUserName.Equals(userList.ListUsers[i].UserName))
                            {
                                userList.ListUsers[i].Friends.Add(friends);
                                break;
                            }
                        }
                        index++;
                    }

                }

                //foreach (var item in userList.ListUsers)
                //{
                //    //Console.WriteLine("用户名：" + item.UserName + ",密码：" + item.Password + ",昵称：" + item.NickName + ",座右铭：" + item.Motto + ",头像：" + item.Head);
                //    foreach (var item2 in item.Friends)
                //    {
                //        Console.Write("你的好友有：" + item2.FriendName + "\n");
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
