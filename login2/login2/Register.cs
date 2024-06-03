using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace login2
{
    public partial class Register : Form
    {
        private string phone; // 电话
        private string password; // 密码
        private string password2; // 确认密码
        private string userName; // 用户名
        private int code; // 输入的验证码
        private int getCode; // 发送的验证码
        public Register()
        {
            InitializeComponent();
            textBox1.LostFocus += new EventHandler(phone_LostFocus);
            textBox1.GotFocus += new EventHandler(phone_GotFocus);
            textBox2.LostFocus += new EventHandler(pwd_LostFocus);
            textBox2.GotFocus += new EventHandler(pwd_GotFocus);
            textBox4.LostFocus += new EventHandler(quren_LostFocus);
            textBox4.GotFocus += new EventHandler(quren_GotFocus);
            textBox3.LostFocus += new EventHandler(valid_LostFocus);
            textBox3.GotFocus += new EventHandler(valid_GotFocus);
            
            button1.Click += new EventHandler(button1_Click);
        }


        private void tishi()
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "请输入手机号";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "请输入密码";
                textBox2.PasswordChar = '\0'; //清空PasswordChar设置

            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "请确认密码";
                textBox4.PasswordChar = '\0'; //清空PasswordChar设置
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "请输入验证码";
            }
        }
        private void phone_GotFocus(object sender, EventArgs e)
        {
            panel1.BackColor = Color.SkyBlue;
            panel2.BackColor = Color.Gray;
            panel3.BackColor = Color.Gray;
            panel4.BackColor = Color.Gray;
            if (textBox1.Text == "请输入手机号")
            {
                textBox1.Text = "";
            }
        }

        private void phone_LostFocus(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Gray;
            if (textBox1.Text == "")
            {
                textBox1.Text = "请输入手机号";
            }
            if (textBox1.Text != "请输入手机号")
            {
                var re = @"^1\d{10}$";
                if(!Regex.IsMatch(textBox1.Text, re)){
                    MessageBox.Show("手机号不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void pwd_GotFocus(object sender, EventArgs e)
        {
            textBox2.PasswordChar = char.Parse("*");
            if (textBox2.Text == "请输入密码")
            {
                textBox2.Text = "";
            }
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.SkyBlue;
            panel3.BackColor = Color.Gray;
            panel4.BackColor = Color.Gray;
        }

        private void pwd_LostFocus(object sender, EventArgs e)
        {
            
            if (textBox2.Text == "")
            {
                textBox2.Text = "请输入密码";
                textBox2.PasswordChar = '\0';
            }
            
            panel2.BackColor = Color.Gray;
        }

        private void quren_GotFocus(object sender, EventArgs e)
        {
            textBox4.PasswordChar = char.Parse("*");
            if (textBox4.Text == "请确认密码")
            {
                textBox4.Text = "";
            }
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.Gray;
            panel3.BackColor = Color.SkyBlue;
            panel4.BackColor = Color.Gray;
        }

        private void quren_LostFocus(object sender, EventArgs e)
        {
            //textBox4.PasswordChar = char.Parse("*"); //设置密码框显示字符为*
            if (textBox4.Text == "")
            {
                textBox4.Text = "请确认密码";
                textBox4.PasswordChar = '\0';
            }
            panel3.BackColor = Color.Gray;
            string querenTxt = textBox4.Text;
            string pwdTxt = textBox2.Text;
            if(querenTxt != pwdTxt&&querenTxt!= "请确认密码" && pwdTxt!= "请输入密码")
            {
                MessageBox.Show("两次输入的密码不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void valid_GotFocus(object sender, EventArgs e)
        {
            if (textBox3.Text == "请输入验证码")
            {
                textBox3.Text = "";
            }
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.Gray;
            panel3.BackColor = Color.Gray;
            panel4.BackColor = Color.SkyBlue;
        }

        private void valid_LostFocus(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "请输入验证码";
            }
            panel4.BackColor = Color.Gray;
        }

        // 发送验证码
        private void button2_Click(object sender, EventArgs e)
        {
            phone = textBox1.Text;
            getCode = CallPhone(phone);
            Console.WriteLine("getCode:" + getCode);
        }

        // 注册
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            /*if (!CallPhone(textBox1.Text.ToString()))
            {
                label1.Text = "不成功";
            }*/

            phone = textBox1.Text;
            password = textBox2.Text;
            password2 = textBox4.Text;
            code = int.Parse(textBox3.Text);
            Console.WriteLine("code:" + code);

            // 获取随机的用户名
            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);

            if (phone != "" && (password.Equals(password2)) && password != "" && password2 != "" && (code == getCode))
            {
                MessageBox.Show("注册成功，您的账号是" + randomNumber, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userName = randomNumber.ToString(); // 获取用户名

                // 数据库连接
                string connectionString = "Server='localhost';Database='db1';User='root';Password='123456'";
                MySqlConnection connection = new MySqlConnection(connectionString);

                try
                {
                    connection.Open();
                    string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
                    string sql = "INSERT INTO userslist (userName,password,phone,nickName,motto,head,isOnline) VALUES (@值1,@值2,@值3,@值4,@值5,@值6,@值7)";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@值1", userName);
                    cmd.Parameters.AddWithValue("@值2", pwd);
                    cmd.Parameters.AddWithValue("@值3", phone);
                    //设置默认值
                    cmd.Parameters.AddWithValue("@值4", "晨曦");
                    cmd.Parameters.AddWithValue("@值5", "加油");
                    cmd.Parameters.AddWithValue("@值6", "C:\\picture\\touxiang.png");
                    cmd.Parameters.AddWithValue("@值7", 0);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("数据插入成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                Login form = new Login(); // 跳转到登陆页面
                form.Show();
                this.Close();
            }else
            {
                    MessageBox.Show("注册失败");
            }

        }

        // 窗体加载
        private void Form2_Load(object sender, EventArgs e)
        {
            tishi();
        }

        // 调用第三方API获取手机验证码
        private int CallPhone(string Recipient_Mobile_Num)
        {
            string PostUrl = "http://106.ihuyi.com/webservice/sms.php?method=Submit";
            //登录“互亿无线网站”查看用户名 登录用户中心->验证码通知短信>产品总览->API接口信息->APIID
            string account = "C69720494";
            //登录“互亿无线网站”查看密码 登录用户中心->验证码通知短信>产品总览->API接口信息->APIKEY
            string password = "b8b0c5e00c1d62b59a2d058c0f2278dd";
            //接收短信的用户的手机号码
            string mobile = Recipient_Mobile_Num;
            //随机生成四位数 可以模仿向用户发送验证码
            Random rad = new Random();
            int mobile_code = rad.Next(1000, 10000);   //生成随机数
            string content = "您的验证码是：" + mobile_code + " 。请不要把验证码泄露给其他人。";

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";  //用户名+密码+注册的手机号+验证码

            UTF8Encoding encoding = new UTF8Encoding();  //万国码
            //将 account, password, mobile, content 这四个内容添加到postStrTpl字符串当中
            //并利用encoding.GetBytes()将括号里面的字符串转化为二进制类型
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, content)); //将字符串postStrTpl中的格式项替换为四个个指定的 Object 实例的值的文本等效项。再转为二进制数据

            //新建一个请求对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);//对统一资源标识符 (URI) 发出请求。 这是一个 abstract 类。
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;

            Stream newStream = myRequest.GetRequestStream();
            //间postData合并到 PostUrl中去
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();


            //以http://106.ihuyi.com/webservice/sms.php?method=Submit&account=你的APIID&password=你的APIKEY&mobile=接收短信的用户的手机号码&content=您的验证码是：" + mobile_code + " 。请不要把验证码泄露给其他人。"    发起https请求   并获取请求结果
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                return mobile_code;
            }
            else
            {
                return -1;
                //访问失败
            }
        }
    }
}
